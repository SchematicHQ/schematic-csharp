#!/bin/bash
set -euo pipefail

# Builds the real NuGet artifact from the working tree and drops it in a local
# feed, for the `pack` install mode of the shared SDK E2E harness
# (SchematicHQ/actions .github/workflows/sdk-e2e.yml).
#
# The `local` mode builds testapp against the source tree through a
# <ProjectReference>, which never exercises packaging. This script closes that
# gap: pack -> local feed -> restore -> run is the same path an end user takes,
# so a package that builds fine from source but ships broken (most importantly
# a missing embedded rulesengine.wasm, which silently degrades every flag check
# to its default) fails pre-merge instead of after publishing.
#
# Produces:    artifacts/local-feed/SchematicHQ.Client.0.0.0-e2e.nupkg
# Consumed by: testapp/Testapp.csproj with -p:SchematicSdkSource=pack
#              (see testapp/nuget.config for the feed registration)
#
# Usage: ./scripts/e2e-pack.sh

SCRIPT_DIR="$(cd "$(dirname "$0")" && pwd)"
REPO_ROOT="$(cd "$SCRIPT_DIR/.." && pwd)"
CSPROJ="$REPO_ROOT/src/SchematicHQ.Client/SchematicHQ.Client.csproj"
WASM="$REPO_ROOT/src/SchematicHQ.Client/RulesEngine/Wasm/rulesengine.wasm"
FEED_DIR="$REPO_ROOT/artifacts/local-feed"
PKG_VERSION="${SCHEMATIC_E2E_PKG_VERSION:-0.0.0-e2e}"

file_size() {
    stat -f%z "$1" 2>/dev/null || stat -c%s "$1"
}

# The WASM is not tracked in git; fetch it so pack can embed it.
"$SCRIPT_DIR/download-wasm.sh"

if [ ! -f "$WASM" ]; then
    echo "ERROR: $WASM missing after download-wasm.sh" >&2
    exit 1
fi
WASM_SIZE=$(file_size "$WASM")
# Sanity-check the input before using it as the yardstick below.
if [ "$WASM_SIZE" -lt 1000 ] || [ "$(head -c 4 "$WASM")" != "$(printf '\0asm')" ]; then
    echo "ERROR: $WASM is not a WebAssembly module (${WASM_SIZE} bytes)" >&2
    exit 1
fi

# Pack exactly what a release packs. An SDK cannot build a target framework
# newer than itself, so fail early and clearly rather than mid-pack.
# SCHEMATIC_E2E_PACK_TFMS overrides the set with a single framework, for local
# runs on an older SDK; it makes the package partial, so CI must not use it.
DECLARED_TFMS=$(dotnet msbuild "$CSPROJ" -getProperty:TargetFrameworks -nologo | tr -d '[:space:]')
PACK_TFMS="${SCHEMATIC_E2E_PACK_TFMS:-$DECLARED_TFMS}"
SDK_MAJOR=$(dotnet --version | cut -d. -f1)

for tfm in ${PACK_TFMS//;/ }; do
    case "$tfm" in
        net[0-9]*.[0-9]*) ;;
        *) continue ;;
    esac
    major=${tfm#net}
    major=${major%%.*}
    if [ "$major" -gt "$SDK_MAJOR" ]; then
        echo "ERROR: SchematicHQ.Client targets $tfm but the installed .NET SDK is ${SDK_MAJOR}.x." >&2
        echo "       Install a ${major}.x or newer SDK (CI: raise dotnet-version in the caller" >&2
        echo "       of SchematicHQ/actions .github/workflows/sdk-e2e.yml)." >&2
        exit 1
    fi
done

echo "Packing SchematicHQ.Client v${PKG_VERSION} ($PACK_TFMS) -> ${FEED_DIR}"
mkdir -p "$FEED_DIR"
# Clear stale packages but keep the directory (and its .gitkeep) in place.
rm -f "$FEED_DIR"/*.nupkg
# The package version never changes, so an extracted copy from an earlier run
# would be restored ahead of the one built here.
rm -rf "${NUGET_PACKAGES:-$HOME/.nuget/packages}/schematichq.client/$PKG_VERSION"

PACK_ARGS=()
if [ -n "${SCHEMATIC_E2E_PACK_TFMS:-}" ]; then
    PACK_ARGS+=(-p:TargetFrameworks="$SCHEMATIC_E2E_PACK_TFMS")
fi

# The csproj derives AssemblyVersion/FileVersion from Version, and those only
# accept numeric versions, so pin them alongside the prerelease package version.
dotnet pack "$CSPROJ" \
    -c Release \
    -p:Version="$PKG_VERSION" \
    -p:AssemblyVersion=0.0.0.0 \
    -p:FileVersion=0.0.0.0 \
    "${PACK_ARGS[@]+"${PACK_ARGS[@]}"}" \
    -o "$FEED_DIR"

NUPKG="$FEED_DIR/SchematicHQ.Client.${PKG_VERSION}.nupkg"
if [ ! -f "$NUPKG" ]; then
    echo "ERROR: expected package not produced: $NUPKG" >&2
    ls -la "$FEED_DIR" >&2
    exit 1
fi

# Guard: the rules engine WASM is an embedded manifest resource inside
# SchematicHQ.Client.dll, not a loose package entry, so check every packaged DLL
# for the WASM bytes themselves. Without them the SDK loads no rules engine and
# silently answers every flag check with its default.
TMP="$(mktemp -d)"
trap 'rm -rf "$TMP"' EXIT
unzip -o -q "$NUPKG" -d "$TMP"

CHECKED=0
for tfm in ${PACK_TFMS//;/ }; do
    DLL="$TMP/lib/$tfm/SchematicHQ.Client.dll"
    if [ ! -f "$DLL" ]; then
        echo "ERROR: lib/$tfm/SchematicHQ.Client.dll missing from $NUPKG" >&2
        exit 1
    fi
    DLL_SIZE=$(file_size "$DLL")

    if command -v perl > /dev/null 2>&1; then
        # An embedded resource is stored verbatim, so the whole WASM file must
        # appear byte for byte inside the assembly.
        if ! perl -0777 -e '
            open(my $w, "<:raw", $ARGV[0]) or die "$ARGV[0]: $!";
            open(my $d, "<:raw", $ARGV[1]) or die "$ARGV[1]: $!";
            local $/;
            exit(index(scalar <$d>, scalar <$w>) >= 0 ? 0 : 1);
        ' "$WASM" "$DLL"; then
            echo "ERROR: rulesengine.wasm is not embedded in lib/$tfm/SchematicHQ.Client.dll." >&2
            echo "       A package like this silently falls back to flag defaults at runtime." >&2
            exit 1
        fi
    elif [ "$DLL_SIZE" -lt "$WASM_SIZE" ]; then
        # No perl: fall back to the crude check that the assembly is at least as
        # large as the WASM it must contain.
        echo "ERROR: lib/$tfm/SchematicHQ.Client.dll is ${DLL_SIZE} bytes, smaller than" >&2
        echo "       rulesengine.wasm (${WASM_SIZE} bytes) — the WASM is not embedded." >&2
        exit 1
    fi

    echo "OK: lib/$tfm/SchematicHQ.Client.dll (${DLL_SIZE} bytes, WASM embedded)"
    CHECKED=$((CHECKED + 1))
done

echo "Packed $NUPKG ($CHECKED framework(s) verified)"
