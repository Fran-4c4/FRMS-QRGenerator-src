# Repository instructions

## Product boundary

- Keep this repository independently buildable and understandable.
- Keep migration notes, source comparisons and publication planning in the
  adjacent private workspace, not in this product repository.
- Do not add dependencies on files outside this repository.

## Build and dependencies

- Target .NET 10 for Windows and keep the solution buildable with `dotnet`.
- Add only dependencies with a documented origin, version, license and use.
- Do not commit generated binaries, package caches or local SDK installations.
- Cover QR encoding changes with deterministic tests that use synthetic data.

## Publication safety

- Never commit credentials, private keys, certificates, personal data or
  customer payloads.
- Run `scripts/Test.ps1` and `scripts/Test-PublicationReadiness.ps1` before a
  public release.
