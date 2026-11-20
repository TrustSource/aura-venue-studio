# Redistributables installed with Venue Studio

The setup bundle (`../VenueStudio.Bundle/Bundle.wxs`) installs these third-party run-times before
Venue Studio itself. They are proprietary redistributables of their vendor and are **not stored in
this repository**. A build fetches them from the vendor into this directory.

| File expected here | What it is | Vendor | Version pinned for Venue Studio 1.0.0 |
|---|---|---|---|
| `windowsdesktop-runtime-10.0.0-win-x64.exe` | .NET Desktop Runtime, needed because Venue Studio is a framework-dependent .NET application | Microsoft | 10.0.0 |
| `MicrosoftEdgeWebView2RuntimeInstallerX64.exe` | Microsoft Edge WebView2 Runtime (fixed-version offline installer), renders the web front end | Microsoft | to be fixed |

These run-times reach the customer's PC through our installer. They appear in neither
`packages.lock.json` nor `package-lock.json`.
