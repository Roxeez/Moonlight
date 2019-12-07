using System.Linq;
using NtCore.API.Commands;
using NtCore.API.Enums;
using NtCore.API.Game.Entities;
using NtCore.API.Game.Maps;

namespace NtCore.Example
{
    public class ExampleCommandHandler : ICommandHandler
    {
        [Command("ping")]
        public void OnPingCommand(ICharacter character)
        {
            character.ShowMessage("pong", MessageType.MIDDLE_SCREEN_YELLOW);
        }

        [Command("mapinfo")]
        public void OnMapInfoCommand(ICharacter character)
        {
            IMap map = character.Map;
            
            character.ShowChatMessage($"-======= MAP {map.Id} =======-", ChatMessageColor.GREEN);
            character.ShowChatMessage($"Players : {map.Players.Count()}", ChatMessageColor.YELLOW);
            character.ShowChatMessage($"Monsters : {map.Monsters.Count()}", ChatMessageColor.YELLOW);
            character.ShowChatMessage($"Npcs : {map.Npcs.Count()}", ChatMessageColor.YELLOW);
            character.ShowChatMessage($"Drops : {map.Drops.Count()}", ChatMessageColor.YELLOW);
            character.ShowChatMessage($"-==================-", ChatMessageColor.GREEN);
        }
    }
}