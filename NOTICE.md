# Notice

Aura Venue Studio, part of the Aura Venue example by TrustSource.

Our own work in this repository is licensed under Creative Commons Attribution-ShareAlike 4.0
International (CC BY-SA 4.0), https://creativecommons.org/licenses/by-sa/4.0/.

## Third-party components

Each component remains under its own licence. The machine-readable list is
[SBOM.cdx.json](SBOM.cdx.json). Licence and copyright information below is taken from the package
metadata (NuGet package specification, npm lock file) as of 2026-11-20.

### Shipped with Venue Studio

| Component | Version | Licence | Copyright or author | Referenced in | Source |
|---|---|---|---|---|---|
| CommunityToolkit.Mvvm | 8.2.2 | MIT | (c) .NET Foundation and Contributors | `src/VenueStudio.Desktop` | https://github.com/CommunityToolkit/dotnet |
| Microsoft.Web.WebView2 | 1.0.2210.55 | Microsoft WebView2 SDK licence (LICENSE.txt in the package) | (c) Microsoft Corporation | `src/VenueStudio.Desktop` | https://aka.ms/webview |
| Newtonsoft.Json | 12.0.3 | MIT | Copyright (c) James Newton-King 2008 | `src/VenueStudio.Desktop` | https://www.newtonsoft.com/json |
| Serilog | 3.1.1 | Apache-2.0 | Copyright (c) 2013-23 Serilog Contributors | `src/VenueStudio.Desktop` | https://serilog.net/ |
| SharpZipLib | 1.3.1 | MIT | Copyright (c) 2000-2020 SharpZipLib Contributors | `src/VenueStudio.Desktop` | https://github.com/icsharpcode/SharpZipLib |
| lodash | 4.17.20 | MIT | see package | `src/frontend` | https://www.npmjs.com/package/lodash/v/4.17.20 |
| monaco-editor | 0.34.0 | MIT | see package | `src/frontend` | https://www.npmjs.com/package/monaco-editor/v/0.34.0 |
| react | 18.2.0 | MIT | see package | `src/frontend` | https://www.npmjs.com/package/react/v/18.2.0 |
| react-dom | 18.2.0 | MIT | see package | `src/frontend` | https://www.npmjs.com/package/react-dom/v/18.2.0 |
| js-tokens | 4.0.0 | MIT | see package | `src/frontend` | https://www.npmjs.com/package/js-tokens/v/4.0.0 |
| loose-envify | 1.4.0 | MIT | see package | `src/frontend` | https://www.npmjs.com/package/loose-envify/v/1.4.0 |
| scheduler | 0.23.2 | MIT | see package | `src/frontend` | https://www.npmjs.com/package/scheduler/v/0.23.2 |

### Installed together with Venue Studio

Third-party run-times chained by the setup bundle. They are redistributables of their vendor, are
not stored in this repository and stay under the vendor's licence terms.

| Component | Version | Vendor | Referenced in |
|---|---|---|---|
| .NET Desktop Runtime | 10.0.0 | Microsoft | `installer/VenueStudio.Bundle/Bundle.wxs` |
| Microsoft Edge WebView2 Runtime | not fixed yet | Microsoft | `installer/VenueStudio.Bundle/Bundle.wxs` |

### Used to build, not shipped

| Component | Version | Licence | Used for |
|---|---|---|---|
| WiX Toolset (`WixToolset.Sdk`, `WixToolset.Util.wixext`) | 6.0.2 | MS-RL | builds the installer |
| WiX bootstrapper application (`WixToolset.BootstrapperApplications.wixext`) | 6.0.2 | MS-RL | becomes part of the setup executable |
| vite and 29 further development packages | 4.4.0 | MIT (vite) | builds the web front end |

The WiX project asks commercial users of its released binaries for a maintenance fee. Nothing built
with it is distributed from this repository.
