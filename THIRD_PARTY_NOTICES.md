# Third-party notices

This inventory records the direct third-party dependencies used by the source
build. Package dependencies are restored from NuGet and are not committed as
copied binaries.

| Component | Version | Source and license | Use |
| --- | --- | --- | --- |
| QRCoder | 1.9.0 | [NuGet](https://www.nuget.org/packages/QRCoder/1.9.0), MIT | QR encoding and PNG rendering |
| xUnit.net v3 | 3.2.2 | [NuGet](https://www.nuget.org/packages/xunit.v3/3.2.2), Apache-2.0 | Automated tests |
| xUnit.net Visual Studio runner | 3.1.5 | [NuGet](https://www.nuget.org/packages/xunit.runner.visualstudio/3.1.5), Apache-2.0 | Test discovery and execution |
| Microsoft.NET.Test.Sdk | 18.12.0 | [NuGet](https://www.nuget.org/packages/Microsoft.NET.Test.Sdk/18.12.0), MIT | .NET test host integration |

The application also depends on the .NET 10 Windows Desktop runtime, which is
distributed separately under Microsoft's .NET licensing terms.
