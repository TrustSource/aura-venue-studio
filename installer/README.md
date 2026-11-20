# Installer

Windows installer of Venue Studio, built with the [WiX Toolset](https://wixtoolset.org/) (version
6.0.2, pulled in as the MSBuild SDK `WixToolset.Sdk`).

| Project | Result | Content |
|---|---|---|
| `VenueStudio.Package` | `VenueStudio.msi` | the desktop application, its NuGet assemblies and the built web front end |
| `VenueStudio.Bundle` | `VenueStudioSetup.exe` | chains the third-party run-times in `redist/` and the MSI |

The installer can only be built on Windows. It has not been built; the files show the structure of
a real installer project.
