# Moonlight

Moonlight aims to make NosTale .NET Application developer life easier by giving them access to a complete & easy to use API allowing them to interact with (almost) everything in the game  
Moonlight can be used with local client (injected .dll) or remote client (clientless)
<br><br>
![Codacy grade](https://img.shields.io/codacy/grade/d7ecbcba4d48445f8a7e12f1bb4fb8e7?style=flat-square)
![AppVeyor](https://img.shields.io/appveyor/build/Roxeez/Moonlight?style=flat-square)
![AppVeyor tests](https://img.shields.io/appveyor/tests/Roxeez/Moonlight?style=flat-square)
![GitHub commit activity](https://img.shields.io/github/commit-activity/m/Roxeez/Moonlight?style=flat-square)
![GitHub](https://img.shields.io/github/license/Roxeez/Moonlight?style=flat-square)

## Getting Started

- Create a new .NET library project targeting .NET Framework 4.7+
- Add Moonlight.dll as dependency
- Install DllExport to your project using DllExport.bat
- Build your application
- Create your database.db sqlite file using Toolkit parser
- Copy your generated .dll & dependencies to your NosTale folder
- Copy database.db & NtNative.dll to your NosTale folder in a subdirectory named Moonlight
- Inject your generated .dll to NosTale process
- Enjoy

> <sub><sup>Since Moonlight use only packets (no memory reading) for compatibility with local & remote client, your dll need to be injected BEFORE selecting your character</sub></sup>

## Example

```csharp
while (isRunning)
{
    Character character = Client.Character;
    Map map = character.Map;
    Skill skill = character.Skills.First();
    
    // Keep in memory character start position
    Position startPosition = character.Position;
    
    // Get all monsters in a radius of 10 around start position
    IEnumerable<Monster> monsters = map.Monsters
        .Where(x => x.Position.IsInRange(startPosition, RADIUS))
        .OrderBy(x => x.Position.GetDistance(character.Position));
    
    // Kill all monsters found
    foreach (Monster monster in monsters)
    {
        while (monster.HpPercentage > 0 && isRunning)
        {
            character.Attack(skill, monster);
        }
    }

    // Get all drops in a radius of 10 around start position
    IEnumerable<Drop> drops = map.Drops
        .Where(x => x.Position.IsInRange(startPosition, RADIUS))
        .OrderBy(x => x.Position.GetDistance(character.Position));
    
    // Pickup all drops found
    foreach (Drop drop in drops)
    {
        if (!character.Equals(drop.Owner))
        {
            return;
        }
        
        character.PickUp(drop);
    }
}
```

### Prerequisites

- **.NET Framework 4.7**

## Contributors
* **Roxeez**

### Special thanks

* **Pumba98** for helping me with some C++/RE related stuff

### License

This project is licensed under the GPL-3.0 License - see the [LICENSE.md](LICENSE.md) file for details
