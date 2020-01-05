# NtCore

NtCore aims to make NosTale .NET Application developer life easier by giving them access to a complete & easy to use API allowing them to interact with (almost) everything in the game  
NtCore can be used with local client (injected .dll) or remote client (clientless)
<br><br>
![Codacy grade](https://img.shields.io/codacy/grade/d7ecbcba4d48445f8a7e12f1bb4fb8e7?style=flat-square)
![GitHub Workflow Status](https://img.shields.io/github/workflow/status/Roxeez/NtCore/Main.Legacy?style=flat-square)
![GitHub top language](https://img.shields.io/github/languages/top/Roxeez/NtCore?style=flat-square)
![GitHub commit activity](https://img.shields.io/github/commit-activity/m/Roxeez/NtCore?style=flat-square)
![GitHub](https://img.shields.io/github/license/Roxeez/NtCore?style=flat-square)
![Maintenance](https://img.shields.io/maintenance/yes/2020?style=flat-square)

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
> This example is simple bot, killing monsters around & pickup drops of player.  
You can find a full example [HERE](https://github.com/Roxeez/NtCore.Example)
```csharp
public class MyApplication
{
    public void Run()
    {
        Kernel32.AllocConsole();

        IClientManager clientManager = NtCoreAPI.GetClientManager();
        IEventManager eventManager = NtCoreAPI.GetEventManager();
        ICommandManager commandManager = NtCoreAPI.GetCommandManager();

        IClient localClient = clientManager.CreateLocalClient();

        var bot = new Bot();

        eventManager.RegisterEventListener(bot, localClient);
        commandManager.RegisterCommandHandler(bot);

        string command;
        do
        {
            command = Console.ReadLine();
        } 
        while (command != "exit");
    }
}

public class Bot : IEventListener, ICommandHandler
{
    private bool _pickUpDrops;
    private bool _isRunning;

    private Task _worker;

    [Handler]
    public async void OnItemDrop(ItemDropEvent e)
    {
        Character character = e.Client.Character;
        Drop drop = e.Drop;

        if (!_isRunning)
        {
            return;
        }

        if (!_pickUpDrops)
        {
            return;
        }

        if (!drop.Owner.Equals(character))
        {
            return;
        }

        await character.WalkTo(drop);
        await character.PickUp(drop);
    }

    [Command("stop")]
    public async void StopCommand(Character sender)
    {
        if (_worker == null || _worker.IsCompleted)
        {
            await sender.ReceiveChatMessage("Not yet started.", ChatMessageColor.RED);
            return;
        }

        _isRunning = false;
        await _worker;
    }

    [Command("start")]
    public async void OnStartCommand(Character sender)
    {
        if (_worker != null)
        {
            await sender.ReceiveChatMessage("Already started.", ChatMessageColor.RED);
            return;
        }

        _worker = Task.Run(() => Work(sender));
        await sender.ReceiveChatMessage("Bot started", ChatMessageColor.RED);
    }

    [Command("pickup")]
    public async void OnPickUpCommand(Character sender)
    {
        _pickUpDrops = !_pickUpDrops;

        await sender.ReceiveChatMessage($"Drop {(_pickUpDrops ? "Enabled" : "Disabled")}", ChatMessageColor.GREEN);
    }

    private async Task Work(Character character)
    {
        _isRunning = true;

        while (_isRunning)
        {
            Map map = character.Map;
            Monster closestMonster = map.Monsters.OrderBy(x => x.Position.GetDistance(character.Position)).FirstOrDefault();
            Skill skill = character.Skills.First();

            if (closestMonster == null)
            {
                continue;
            }

            while (closestMonster.HpPercentage > 0 && _isRunning)
            {
                await character.WalkTo(closestMonster);
                await character.UseSkillOn(skill, closestMonster);

                await Task.Delay(skill.Info.Cooldown * 1000);
            }
        }
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
