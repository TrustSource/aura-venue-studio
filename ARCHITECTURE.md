# Architecture: aura-venue-studio

Version 1.0.0 · 2026-11-20 · Diagram: [ARCHITECTURE.drawio](ARCHITECTURE.drawio)

## System overview and purpose

Venue Studio runs on a normal Windows PC at the planner's or integrator's office. It is used to
describe the installation (zones, amplifiers, sources), to write the control logic and to load both
onto the controller. It is a software product of its own, shipped to many customers, and it is the
part of the system that ordinary software composition analysis handles best.

## Components and responsibilities

| Component | Responsibility | Origin |
|---|---|---|
| Desktop application (`src/VenueStudio.Desktop`) | project handling, project archives, hosts the front end in WebView2 | Aurelia Audio; .NET 10 (WPF) with five NuGet packages |
| Front end (`src/frontend`) | configuration and logic editor | Aurelia Audio; React and the Monaco editor from npm, built with vite |
| Installer (`installer/`) | MSI package and setup bundle, built with the WiX Toolset | Aurelia Audio; WiX 6.0.2 |
| Third-party run-times (`installer/redist`) | .NET Desktop Runtime and WebView2 Runtime, installed by the setup bundle | Microsoft redistributables, not stored in git |
| Sample control application (`venue-plc-app`) | logic of one installation, refers to function libraries by name and version | written by the integrator, not by the manufacturer |

## Key interfaces and data flows

| From | To | What |
|---|---|---|
| Studio | runtime on the controller | download of the control application, online diagnosis |
| Studio | project file | installation description and logic |
| Installer | Windows PC | application plus third-party run-times |

Protocols are not fixed yet.

## Infrastructure and deployment

Nothing is deployed. The repository is input for analysis.

Communication with the TrustSource API (uploads and imports from CI) uses the organisation secret
`AURA_UPLOAD_KEY`, sent as the `x-api-key` header. The base URL including the API version comes from
`TS_API_BASE_URL`; it is not hard-coded. No workflow exists yet.

Mapping to TrustSource: one project "Aura Venue Studio" with modules for the desktop application and
the front end. The sample control application is a separate module, because it belongs to another
party.

## Scan architecture

One TrustSource project "Aura Venue Studio". Module names contain no version, so that settings and
approvals survive releases. `ts-scan` derives the module name from the package itself; there is no
option to set it.

| Module (name as derived by ts-scan) | Scanned path | How | Result of the first local scan (ts-scan 1.10.0, 2026-11-20) |
|---|---|---|---|
| `VenueStudio` | `src/VenueStudio.Desktop` | NuGet scan, upload in TrustSource format | 5 of 5 packages found |
| `venue-studio-frontend` | `src/frontend` | Node scan, upload in TrustSource format | all 7 shipped packages found; the 30 development packages are left out by default |
| `installer` | `installer` | file scan | no component found: neither the WiX packages (no lock file) nor the two run-times chained in `Bundle.wxs` |
| `venue-plc-app` | `examples/venue-plc-app` | file scan | no component found: the five library references are not recognised |

What the scans do not see has to be entered by hand; `truth.yaml` lists it. The installer and the
sample control application are the two places where that is necessary.

`ts-scan-agent` 0.6.2 proposes the same split for desktop application and front end. It also
proposes the solution at the repository root and the two WiX projects as NuGet modules, and asks
whether they are released separately. It does not notice the chained run-times or the sample control
application.

The file scan sends file fingerprints to the SCANOSS service to recognise known code.

## Architecture decision records

### ADR-001: Four repositories, operating system separate

- **Date:** 2026-11-20 (v0.1.0)
- **Status:** Accepted
- **Context:** The example has to reflect how device manufacturers organise their work: a platform
  team builds the Linux, product teams build devices on top of it, and engineering software is a
  product of its own. A single repository would hide exactly the hand-over points that cause the
  difficulties we want to study. A separate operating-system repository also leaves room for OS
  variants.
- **Decision:** Four repositories: `aura-venue-studio`, `aura-venue-controller`, `aura-venue-os`,
  `aura-venue-firmware`. In TrustSource each one is a project of its own. The operating system is
  included in the controller project as a **linked module** (a module that refers to another project
  and one of its approvals). The purchased runtime is a third-party component of the controller.
- **Consequences:** The controller depends on a specific approved state of the operating system, not
  on its source. Cross-repository topics (system overview, measurement log) need one agreed home.
  Linked-module behaviour across OS versions becomes part of what is tested (see aura-venue-os).

### ADR-002: Realistic artefacts instead of running software, measured against a ground truth

- **Date:** 2026-11-20 (v0.1.0)
- **Status:** Accepted
- **Context:** The purpose is to learn where scanning and vulnerability matching succeed and where
  manual work remains. That needs realistic input and a reference to compare with, not a working
  product.
- **Decision:** Repositories contain realistic files (manifests, vendored sources, binaries, images,
  supplier documents) that need not build or run. Each repository carries a `truth.yaml` with the
  real content and the expected vulnerabilities, each entry backed by a source. Only freely licensed
  third-party files are used; no proprietary binaries of real suppliers.
- **Consequences:** Results are reproducible and can be published. Building `truth.yaml` is real
  effort and has to be kept in step with every change to the content.

### ADR-003: The sample control application lives in this repository

- **Date:** 2026-11-20 (v0.1.0)
- **Status:** Proposed
- **Context:** The control application is written by the integrator for one installation. It is not
  part of the manufacturer's product, but it is created with the Studio and its library references
  are a case of their own: names and versions without established package identifiers.
- **Decision:** A sample control application is kept under `examples/venue-plc-app/` and handled in
  TrustSource as a separate module.
- **Consequences:** No fifth repository. The different ownership has to be made clear in the
  documentation and in the TrustSource project structure.

### ADR-004: The installer bundles third-party run-times

- **Date:** 2026-11-20 (v0.1.0)
- **Status:** Accepted
- **Context:** Engineering software for industrial devices often installs third-party run-times along
  with itself, for example for licensing or communication. Vulnerabilities in those run-times reach
  the manufacturer's customers through the manufacturer's installer, although they appear in no
  package manifest.
- **Decision:** The setup bundle chains two third-party run-times that are declared in neither the
  .NET nor the npm manifest: the .NET Desktop Runtime (Venue Studio is framework-dependent) and the
  Microsoft Edge WebView2 Runtime. They are referenced in `Bundle.wxs` and fetched at build time;
  the files are not stored in git, because they are proprietary redistributables.
- **Consequences:** The run-times are invisible to manifest-based scanning; finding and tracking
  them is part of the exercise. Their versions have to be pinned and maintained by hand.

### ADR-005: Installer built with the WiX Toolset

- **Date:** 2026-11-20 (v1.0.0)
- **Status:** Accepted
- **Context:** The Studio needs a Windows installer. The alternatives were Visual Studio installer
  projects and the WiX Toolset.
- **Decision:** WiX Toolset 6.0.2: an MSI package project and a bundle project. WiX is text-based,
  builds through the normal .NET build and is widespread in industry. Its bundle is the usual place
  where manufacturers chain third-party run-times.
- **Consequences:** WiX arrives as a NuGet package, so a build tool appears among the dependencies.
  It is licensed under MS-RL, and the WiX project asks commercial users of its released binaries for
  a maintenance fee; nothing built with it is distributed from this repository. The installer can
  only be built on Windows and has not been built.

### ADR-006: Desktop application hosts a web front end

- **Date:** 2026-11-20 (v1.0.0)
- **Status:** Proposed
- **Context:** Engineering tools often combine a native shell with a web-based editor.
- **Decision:** A .NET desktop application (WPF) hosts a React front end in WebView2.
- **Consequences:** One product with two package ecosystems (NuGet and npm) and an additional
  run-time (WebView2) on the customer's PC.

### ADR-007: Venue Studio 1.0.0 deliberately contains outdated dependencies

- **Date:** 2026-11-20 (v1.0.0)
- **Status:** Accepted
- **Context:** The example is to show the handling of vulnerabilities over two releases: a statement
  that a vulnerability does not apply (VEX), a fix in a following release and a security advisory.
- **Decision:** Release 1.0.0 pins Newtonsoft.Json 12.0.3, SharpZipLib 1.3.1 and lodash 4.17.20,
  each with known vulnerabilities recorded in `truth.yaml`. The code uses them so that one case is
  really reachable (extraction of project archives with SharpZipLib) and one is not (the affected
  lodash functions are not imported).
- **Consequences:** The repository triggers vulnerability alerts by design. It must not be used as a
  basis for real software.

## Open decisions

- Protocol between Studio and controller.
- Version of the WebView2 Runtime to pin; known vulnerabilities of the pinned .NET Desktop Runtime.
- Whether development and build tools (vite, WiX) belong in the product's bill of materials.
- How the installer and the sample control application get into TrustSource (hand-maintained entries).
