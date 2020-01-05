﻿using NtCore.Clients;
using NtCore.Events;
using NtCore.Events.Character;
using NtCore.Extensions;
using NtCore.Game.Battle;
using NtCore.Game.Entities;
using NtCore.Network.Packets.Characters;

namespace NtCore.Network.Handlers.Characters
{
    public class NcifPacketHandler : PacketHandler<NcifPacket>
    {
        private readonly IEventManager _eventManager;

        public NcifPacketHandler(IEventManager eventManager)
        {
            _eventManager = eventManager;
        }
        
        public override void Handle(IClient client, NcifPacket packet)
        {
            var character = client.Character.As<Character>();
            var entity = character.Map.GetEntity(packet.EntityType, packet.EntityId).As<ILivingEntity>();

            if (entity == null)
            {
                return;
            }

            ITarget currentTarget = character.Target;
            
            character.Target = new Target(entity);
            
            if (currentTarget != null && !currentTarget.Entity.Equals(entity))
            {
                _eventManager.CallEvent(new TargetChangeEvent(client));
            }
        }
    }
}