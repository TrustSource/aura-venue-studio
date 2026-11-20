# Architecture: aura-venue-studio

Version 0.1.0 · 2026-11-20 · Diagram: [ARCHITECTURE.drawio](ARCHITECTURE.drawio)

## System overview and purpose

Venue Studio runs on a normal Windows PC at the planner's or integrator's office. It is used to
describe the installation (zones, amplifiers, sources), to write the control logic and to load both
onto the controller. It is a software product of its own, shipped to many customers, and it is the
part of the system that ordinary software composition analysis handles best.

## Components and responsibilities

| Component | Responsibility | Origin |
|---|---|---|
| Desktop application | project handling, communication with the controller | Aurelia Audio, .NET packages |
| Front end | configuration and logic editor | Aurelia Audio, npm packages |
| Installer | installs the application and third-party run-times it needs | Aurelia Audio, bundled third-party installers |
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
- **Status:** Proposed
- **Context:** Engineering software for industrial devices often installs third-party run-times along
  with itself, for example for licensing or communication. Vulnerabilities in those run-times reach
  the manufacturer's customers through the manufacturer's installer, although they appear in no
  package manifest.
- **Decision:** The installer of the Studio carries at least one third-party run-time that is not
  declared in the .NET or npm manifests.
- **Consequences:** The component is invisible to manifest-based scanning; finding it is part of the
  exercise.

## Open decisions

- Protocol between Studio and controller.
- Which third-party run-time the installer bundles (freely licensed stand-in).
