# testapp-packaged — packaged-artifact E2E variant

This is a second copy of the SDK E2E test app that consumes `SchematicHQ.Client`
as a **NuGet package** instead of a `<ProjectReference>`. It exists to catch
packaging/install failures that the source-built `../testapp` cannot.

## Why

The rules engine now runs inside a WebAssembly binary (`rulesengine.wasm`) that is
**embedded in the SDK DLL** and executed by the **Wasmtime native library**
(`libwasmtime`), which ships as a transitive, per-RID native asset of the `Wasmtime`
NuGet package. There are several links in the chain where that can break between
`dotnet build` on a dev machine and a real end-user install:

- `dotnet pack` producing a DLL that is missing the embedded `.wasm`
- the NuGet package omitting or mis-declaring the `Wasmtime` dependency
- `libwasmtime` failing to resolve for the consumer's RID at restore/publish time
  (e.g. `linux-musl-x64` / Alpine, `linux-arm`, `win-x86` are **not** shipped by
  Wasmtime)

`../testapp` builds the SDK from local source, so it exercises none of these — its
green run only proves the working tree compiles. This variant restores the built
`.nupkg` from a local feed, so a datastream-mode flag check here runs through the
**packaged** wasm + Wasmtime exactly as an end user would.

## How it works

1. `scripts/e2e-pack.sh` packs the SDK (`-p:Version=0.0.0-e2e`) into
   `artifacts/local-feed/` and asserts the embedded wasm survived (DLL size guard).
2. `nuget.config` here adds that folder as a package source.
3. `Testapp.Packaged.csproj` links `../testapp/Program.cs` (identical app logic) and
   references `SchematicHQ.Client` `0.0.0-e2e` from the local feed.

## Run locally

```bash
# from repo root — needs gh auth to the private schematic-api repo for the wasm
./scripts/e2e-pack.sh
dotnet build testapp-packaged/Testapp.Packaged.csproj -c Release
dotnet run   --project testapp-packaged/Testapp.Packaged.csproj --no-build -c Release
# then, in another shell, drive it with actions/sdk-e2e/run-local.sh (datastream mode):
#   E2E_API_KEY=api_xxx ./run-local.sh '{"useDataStream": true}'
```

## Wire into the shared suite

Add a matrix entry alongside the existing `csharp` one in
`schematic-api/.github/workflows/sdk_e2e.yml` so the packaged artifact is smoke-tested
in a wasm-backed (datastream) mode on every run:

```yaml
  csharp-packaged:
    repository: SchematicHQ/schematic-csharp
    dotnet-version: "8.0"
    setup: ./scripts/e2e-pack.sh && dotnet build testapp-packaged/Testapp.Packaged.csproj -c Release
    start: dotnet run --project testapp-packaged/Testapp.Packaged.csproj --no-build -c Release
```

> **RID caveat:** GitHub's `ubuntu-latest` runner is `linux-x64` (glibc) — the RID
> Wasmtime *does* ship. This variant therefore proves the pack/restore/embed chain but
> **not** the platforms most likely to be missing a `libwasmtime` (Alpine/musl, arm,
> Windows, macOS). To cover those, run the packaged testapp inside the target base
> image (e.g. an `mcr.microsoft.com/dotnet/aspnet:8.0-alpine` container) and assert a
> non-default flag result. Because `DatastreamClient` degrades to flag defaults when the
> engine fails to initialize, a missing native lib is silent — the assertion must check
> a rule-derived value, not merely that the call returned.
