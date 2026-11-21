# Contributing to FRMS QR Generator

Contributions should be small, reviewable and accompanied by tests when they
change behavior.

## Development workflow

1. Install the .NET 10 SDK on Windows.
2. Create a branch and make the smallest coherent change.
3. Do not use real customer payloads in source, tests or screenshots.
4. Run `scripts/Test.ps1`.
5. Run `scripts/Test-PublicationReadiness.ps1`.

For packaging changes, also run `scripts/Publish.ps1` and smoke-test the
resulting executable on a Windows system without relying on the repository
layout.

When adding a package, document its exact version, source, license and purpose
in `THIRD_PARTY_NOTICES.md`. Generated output and local package caches do not
belong in the repository.
