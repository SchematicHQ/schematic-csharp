#!/bin/bash
set -euo pipefail

# Packs the SchematicHQ.Client SDK into a local NuGet feed for the packaged-artifact
# E2E variant (testapp-packaged/). This validates the real distribution artifact:
# the embedded rulesengine.wasm and the transitive Wasmtime native library must
# survive pack -> restore -> run, which a plain <ProjectReference> build never
# exercises.
#
# Produces:  artifacts/local-feed/SchematicHQ.Client.<version>.nupkg
# Consumed by: testapp-packaged/Testapp.Packaged.csproj (via testapp-packaged/nuget.config)

SCRIPT_DIR="$(cd "$(dirname "$0")" && pwd)"
REPO_ROOT="$(cd "$SCRIPT_DIR/.." && pwd)"
FEED_DIR="$REPO_ROOT/artifacts/local-feed"
PKG_VERSION="${SCHEMATIC_E2E_PKG_VERSION:-0.0.0-e2e}"

# Ensure the rules engine WASM is present before packing so it gets embedded.
"$SCRIPT_DIR/download-wasm.sh"

echo "Packing SchematicHQ.Client v${PKG_VERSION} -> ${FEED_DIR}"
rm -rf "$FEED_DIR"
mkdir -p "$FEED_DIR"

dotnet pack "$REPO_ROOT/src/SchematicHQ.Client/SchematicHQ.Client.csproj" \
    -c Release \
    -p:Version="$PKG_VERSION" \
    -o "$FEED_DIR"

NUPKG="$FEED_DIR/SchematicHQ.Client.${PKG_VERSION}.nupkg"
if [ ! -f "$NUPKG" ]; then
    echo "ERROR: expected package not produced: $NUPKG" >&2
    exit 1
fi

# Guard: fail loudly if the embedded WASM did not make it into the packaged DLL.
# The wasm (~672 KB) is an embedded manifest resource inside SchematicHQ.Client.dll,
# not a loose package entry, so we verify by DLL size — the exact packaging
# regression this variant exists to catch (an empty/stripped resource ships a
# DLL hundreds of KB smaller).
TMP="$(mktemp -d)"
trap 'rm -rf "$TMP"' EXIT
unzip -o -q "$NUPKG" -d "$TMP"

DLL="$TMP/lib/net8.0/SchematicHQ.Client.dll"
if [ ! -f "$DLL" ]; then
    echo "ERROR: lib/net8.0/SchematicHQ.Client.dll missing from package" >&2
    exit 1
fi

# Cross-platform file size (macOS: stat -f%z, Linux: stat -c%s).
DLL_SIZE=$(stat -f%z "$DLL" 2>/dev/null || stat -c%s "$DLL")
MIN_SIZE=600000 # rulesengine.wasm is ~672 KB; a DLL without it is far smaller.
if [ "$DLL_SIZE" -lt "$MIN_SIZE" ]; then
    echo "ERROR: packaged DLL is ${DLL_SIZE} bytes (< ${MIN_SIZE})." >&2
    echo "       rulesengine.wasm appears NOT embedded — the package would silently" >&2
    echo "       fall back to flag defaults at runtime." >&2
    exit 1
fi

echo "Packed OK: $NUPKG (embedded DLL ${DLL_SIZE} bytes)"
