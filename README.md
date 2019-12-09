# NtCore

NtCore is basically what is Spigot to Minecraft but for NosTale, it allow you to create .NET application using NtCore.API and make them run on NtCore.

NtCore.API give you access to all interface used by NtCore and allow you to create plugin & easily interact with everything in NosTale using a local client (injected) or remote client (clientless).  
Since everything is managed by NtCore, you can easily interact between plugins & running clients in all your plugins
<br><br>
![Codacy grade](https://img.shields.io/codacy/grade/d7ecbcba4d48445f8a7e12f1bb4fb8e7?style=flat-square)
![GitHub Workflow Status](https://img.shields.io/github/workflow/status/Roxeez/NtCore/Main.Legacy?style=flat-square)
![GitHub top language](https://img.shields.io/github/languages/top/Roxeez/NtCore?style=flat-square)
![GitHub last commit](https://img.shields.io/github/last-commit/Roxeez/NtCore?style=flat-square)
![GitHub](https://img.shields.io/github/license/Roxeez/NtCore?style=flat-square)
![Maintenance](https://img.shields.io/maintenance/yes/2019?style=flat-square)

## Getting Started

- Clone
- Open solution
- Build
- Create a new .NET library project targeting .NET Framework 4.7+ & close it
- Copy DllExport.bat to your solution directory
- Run DllExport.bat
- Open your project solution & add Costura Fody as project dependency using Nuget
- Build your application
- Enjoy

### Prerequisites

- ***.NET Framework 4.7*** (until DllExport is updated to .NET Core)
- ***Costura Fody*** (for packing everything in one .dll)

## Contributing

Please read [CONTRIBUTING.md](CONTRIBUTING.md) for details on our code of conduct, and the process for submitting pull requests to us.

## Authors

* **Roxeez**

## License

This project is licensed under the GPL-3.0 License - see the [LICENSE.md](LICENSE.md) file for details
