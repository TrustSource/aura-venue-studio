# Changelog

All notable changes to this project are documented in this file. The format follows
[Keep a Changelog](https://keepachangelog.com/en/1.1.0/), versions follow
[Semantic Versioning](https://semver.org/).

## [Unreleased]

## [1.0.0] - 2026-11-20

First content of Venue Studio. The version number now follows the product release.

### Added

- Desktop application `src/VenueStudio.Desktop` (.NET 10, WPF) with `packages.lock.json`.
- Web front end `src/frontend` (React, Monaco editor, vite) with `package-lock.json`.
- Installer `installer/` built with the WiX Toolset 6.0.2: MSI package and setup bundle that chains
  the .NET Desktop Runtime and the WebView2 Runtime.
- Sample control application `examples/venue-plc-app` with library references.
- Scan architecture and results of the first local scans in `ARCHITECTURE.md`.

### Changed

- `truth.yaml`, `NOTICE.md` and `SBOM.cdx.json` now list the real components; vulnerability entries
  are taken from the OSV database.

### Security

- Release 1.0.0 deliberately pins dependencies with known vulnerabilities (Newtonsoft.Json 12.0.3,
  SharpZipLib 1.3.1, lodash 4.17.20), see ADR-007. Demonstration only.

## [0.1.0] - 2026-11-20

### Added

- Repository structure: README, CHANGELOG, VERSION, ARCHITECTURE.md with ADR section,
  ARCHITECTURE.drawio.
- `truth.yaml` skeleton for the ground truth of this repository.
- Licence statement (CC BY-SA 4.0 for our own work), `NOTICE.md` and `SBOM.cdx.json` (CycloneDX, no
  third-party components yet).
