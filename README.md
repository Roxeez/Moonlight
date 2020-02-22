# Moonlight

Moonlight aims to make NosTale .NET Application developer life easier by giving them access to a complete & easy to use API allowing them to interact with (almost) everything in the game  
Moonlight can be used with local client (injected .dll) or remote client (clientless)
<br><br>
![Codacy grade](https://img.shields.io/codacy/grade/d7ecbcba4d48445f8a7e12f1bb4fb8e7)
![AppVeyor](https://img.shields.io/appveyor/build/Roxeez/Moonlight)
![AppVeyor tests](https://img.shields.io/appveyor/tests/Roxeez/Moonlight)
![GitHub commit activity](https://img.shields.io/github/commit-activity/m/Roxeez/Moonlight)
![GitHub](https://img.shields.io/github/license/Roxeez/Moonlight)

## Getting Started

- Clone Moonlight
- Build Moonlight
- Create a C# .dll project targeting .NET Framework 4.7+
- Install DllExport to your project and create your export function (cf. DllExport wiki)
- Build your project
- Create database.db using Moonlight.Toolkit CLI*
- Copy previously generated database.db & Moon.dll* to a subfolder named Moonlight in your NosTale folder
- Copy your generated .dll to NosTale folder
- Inject your .dll using an injector supporting custom export function.

> <sub><sup>*Moon.dll & Moonlight.Toolkit are located in Moonlight build folder.</sub></sup>  
> Moonlight is a packet based lib, so if you want everything to work correctly using local client, it should be injected before character selection.
## Example
>Example application can be found here : https://github.com/Roxeez/Moonlight.Example
```csharp
private async Task BotLoop()
{
    while (IsRunning)
    {
        IEnumerable<Monster> monsters;
        Skill zoneSkill;
        do
        {
            zoneSkill = Configuration.UsedSkills.FirstOrDefault(x => !x.IsOnCooldown);
            monsters = Client.Character.Map.Monsters
                .Where(x => x.Vnum == MonsterConstants.SoftPii)
                .Where(x => x.Position.IsInRange(Client.Character.Position, Radius))
                .OrderBy(x => x.Position.GetDistance(Client.Character.Position));
            
            Monster closestPod = await GetClosestPod();
            if (closestPod == null)
            {
                return;
            }

            await Client.Character.Attack(closestPod);
        } 
        while ((monsters.Count() < 10 || zoneSkill == null) && IsRunning);

        if (monsters.Count() < 10)
        {
            return;
        }
        
        await Client.Character.UseSkillOn(zoneSkill, monsters.First());
        await Task.Delay(100);
    }
}

private async Task<Monster> GetClosestPod()
{
    Monster pod;
    do
    {
        pod = Client.Character.Map.Monsters
            .Where(x => x.Vnum == MonsterConstants.SoftPiiPod)
            .Where(x => x.Position.IsInRange(Client.Character.Position, Radius))
            .OrderBy(x => x.Position.GetDistance(Client.Character.Position))
            .FirstOrDefault();
        
        await Task.Delay(100);
    } 
    while (pod == null && IsRunning);

    return pod;
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
