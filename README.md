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
Character character = _client.Character;
Skill basicAttack = character.Skills.FirstOrDefault();

while (IsRunning)
{
    IEnumerable<Monster> allPii;
    Skill skill;
    
    do
    {
        Monster pod;
        do
        {
            pod = character.GetClosestMonsterInRadius(Constants.SoftPiiPodVnum, Radius);
            if (pod == null)
            {
                await Task.Delay(100);
            }
        } 
        while (pod == null);

        await character.Attack(basicAttack, pod);
        
        allPii = character.GetClosestMonstersInRadius(Constants.SoftPiiVnum, Radius);
        skill = skills.FirstOrDefault(x => !x.IsOnCooldown);

        await Task.Delay(100);
    } 
    while (allPii.Count() < 10 || skill == null);

    await character.Attack(skill, allPii.First());
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
