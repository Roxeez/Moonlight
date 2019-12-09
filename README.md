# NtCore

NtCore aims to make NosTale .NET Application developer life easier by giving them access to a complete & easy to use API allowing them to interact with (almost) everything in the game  
NtCore can be used with local client (injected .dll) or remote client (clientless)
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
- Install DllExport to your project using DllExport.bat
- Open your project solution & add Costura Fody as project dependency using Nuget
- Build your application
- Enjoy

## Example
> This example code will make character follow a target, full example is available [HERE](https://github.com/Roxeez/NtCore/tree/master/srcs/Example)
```csharp
public class MyApplication
{
    /// <summary>
    /// My application entry point
    /// </summary>
    public void Run()
    {
        // Register our event listener
        NtCoreAPI.Instance.RegisterEventListener<MyEventListener>();

        // Wait for exit command (because i'm using a console and not an UI app)
        string command;
        do
        {
            command = Console.ReadLine();
        } 
        while (command != "exit");
    }
}
    
public class MyEventListener : IEventListener
{
    /// <summary>
    /// This method will be called each time character target move
    /// </summary>
    [Handler]
    public void OnTargetMove(TargetMoveEvent e)
    {
        ICharacter character = e.Character;
        ILivingEntity target = character.Target.Entity;

        character.Move(target.Position);
    }
}
```

### Prerequisites

- ***.NET Framework 4.7*** (until DllExport is updated to .NET Core)
- ***Costura Fody*** (for packing everything in one .dll)

## Contributing

Please read [CONTRIBUTING.md](CONTRIBUTING.md) for details on our code of conduct, and the process for submitting pull requests to us.

## Authors

* **Roxeez**

## License

This project is licensed under the GPL-3.0 License - see the [LICENSE.md](LICENSE.md) file for details
