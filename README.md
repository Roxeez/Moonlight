# NtCore

NtCore aims to make NosTale .NET Application developer life easier by giving them access to a complete & easy to use API allowing them to interact with (almost) everything in the game  
NtCore can be used with local client (injected .dll) or remote client (clientless)
<br><br>
![Codacy grade](https://img.shields.io/codacy/grade/d7ecbcba4d48445f8a7e12f1bb4fb8e7?style=flat-square)
![GitHub Workflow Status](https://img.shields.io/github/workflow/status/Roxeez/NtCore/Main.Legacy?style=flat-square)
![GitHub top language](https://img.shields.io/github/languages/top/Roxeez/NtCore?style=flat-square)
![GitHub commit activity](https://img.shields.io/github/commit-activity/m/Roxeez/NtCore?style=flat-square)
![GitHub](https://img.shields.io/github/license/Roxeez/NtCore?style=flat-square)
![Maintenance](https://img.shields.io/maintenance/yes/2019?style=flat-square)

## Getting Started

- Clone
- Open solution
- Build
- Create a new .NET library project targeting .NET Framework 4.7+
- Add NtCore.dll & Costura Fody as dependency
- Install DllExport to your project using DllExport.bat
- Build your application
- Copy your generated .dll and NtNative.dll to your NosTale folder
- Inject your generated .dll to NosTale process
- Enjoy

> <sub><sup>Since NtCore use only packets (no memory reading) for compatibility with local & remote client, your dll need to be injected BEFORE selecting your character</sub></sup>

## Example
> This example code will make character move to dropped item when they spawn & pick up them.  
You can find a full example [HERE](https://github.com/Roxeez/NtCore.Example)
```csharp
public class MyApplication
{
    /// <summary>
    /// My application entry point
    /// </summary>
    public void Run()
    {
        // Register our event listener
        NtCoreAPI.GetEventManager().RegisterEventListener<MyEventListener>();

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
    [Handler]
    public void OnItemDrop(ItemDropEvent e)
    {
        ICharacter character = e.Client.Character;

        if (e.Drop.Owner != null && character.Equals(e.Drop.Owner))
        {
            return;
        }

        character.Move(e.Drop.Position);
        character.PickUp(e.Drop);
    }
}
```

### Prerequisites

- **.NET Framework 4.7**
- **DllExport** (More information [HERE](https://github.com/3F/DllExport))
- **Costura Fody**

## Contributing

Please read [CONTRIBUTING.md](CONTRIBUTING.md) for details on our code of conduct, and the process for submitting pull requests to us.

## Contributors
* **Roxeez**
* **Pumba98**

## License

This project is licensed under the GPL-3.0 License - see the [LICENSE.md](LICENSE.md) file for details
