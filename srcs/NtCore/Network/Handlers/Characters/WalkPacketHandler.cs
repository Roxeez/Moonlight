using NtCore.API;
using NtCore.API.Clients;
using NtCore.API.Events.Character;
using NtCore.API.Extensions;
using NtCore.API.Plugins;
using NtCore.Game.Entities;
using NtCore.Network.Packets.Characters;

namespace NtCore.Network.Handlers.Characters
{
    public class WalkPacketHandler : PacketHandler<WalkPacket>
    {
        private readonly IPluginManager _pluginManager;
        
        public WalkPacketHandler(IPluginManager pluginManager)
        {
            _pluginManager = pluginManager;
        }
        
        public override void Handle(IClient client, WalkPacket packet)
        {
            var character = client.Character.As<Character>();

            Position from = character.Position;
            
            character.Speed = packet.Speed;
            character.Position = new Position(packet.X, packet.Y);
            
            _pluginManager.CallEvent(new CharacterMoveEvent(client.Character, from));
        }
    }
}