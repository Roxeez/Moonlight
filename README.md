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
private async Task Work(Character character)
{
    _isRunning = true;

    while (_isRunning)
    {
        Position startPosition = character.Position;

        Map map = character.Map;
        IEnumerable<Monster> monsters = map.Monsters.Where(x => x.Position.IsInArea(character.Position, MonsterRadius)).OrderBy(x => x.Position.GetDistance(character.Position));
        Skill skill = character.Skills.First();

        foreach (Monster monster in monsters)
        {
            if (character.HpPercentage < 20)
            {
                await character.ShowBubbleMessage("Resting...");
                await character.Rest();

                do
                {
                    await Task.Delay(1000);
                } 
                while (character.HpPercentage < 100 && character.IsResting);
            }

            await character.ShowBubbleMessageOn($"{monster.Name} Lv.{monster.Level}", monster);

            while (monster.HpPercentage > 0 && _isRunning)
            {
                if (monster.Position.GetDistance(character.Position) > skill.Info.Range)
                {
                    await character.Walk(monster.Position);
                }

                await character.UseSkillOn(skill, monster);

                await Task.Delay(skill.Info.Cooldown * 100);
            }
        }

        await Task.Delay(1000);

        IEnumerable<Drop> drops = map.Drops.Where(x => x.Position.IsInArea(startPosition, DropRadius)).OrderBy(x => x.Position.GetDistance(character.Position));
        foreach (Drop drop in drops)
        {
            if (!character.Equals(drop.Owner))
            {
                continue;
            }

            await character.ShowBubbleMessage($"Trying to pickup {drop.Item.Name}");

            await character.Walk(drop.Position);
            await character.PickUp(drop);

            await Task.Delay(1000);
        }

        await character.Walk(startPosition);
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
