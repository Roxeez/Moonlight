using NtCore.API;
using NtCore.API.Clients;
using NtCore.API.Events.Character;
using NtCore.API.Events.Entity;
using NtCore.API.Extensions;
using NtCore.API.Game.Entities;
using NtCore.API.Plugins;
using NtCore.Game.Entities;
using NtCore.Network.Packets.Entities;

namespace NtCore.Network.Handlers.Entities
{
    public class MvPacketHandler : PacketHandler<MvPacket>
    {
        private readonly IPluginManager _pluginManager;
        
        public MvPacketHandler(IPluginManager pluginManager)
        {
            _pluginManager = pluginManager;
        }
        
        public override void Handle(IClient client, MvPacket packet)
        {
            ICharacter character = client.Character;
            var entity = client.Character.Map.GetEntity(packet.EntityType, packet.EntityId).As<LivingEntity>();

            if (entity == null)
            {
                return;
            }

            Position from = character.Position;
            
            entity.Position = new Position(packet.X, packet.Y);
            entity.Speed = packet.Speed;
            
            _pluginManager.CallEvent(new EntityMoveEvent(entity, from));

            if (character.Target != null && character.Target.Entity.Equals(entity))
            {
                _pluginManager.CallEvent(new TargetMoveEvent(character, from));
            }
        }
    }
}