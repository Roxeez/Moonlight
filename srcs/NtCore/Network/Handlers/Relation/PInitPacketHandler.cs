using System.Collections.Generic;
using System.Linq;
using NtCore.Clients;
using NtCore.Enums;
using NtCore.Extensions;
using NtCore.Game.Entities;
using NtCore.Game.Relation;
using NtCore.Network.Packets.Relation;

namespace NtCore.Network.Handlers.Relation
{
    public class PInitPacketHandler : PacketHandler<PInitPacket>
    {
        public override void Handle(IClient client, PInitPacket packet)
        {
            Character character = client.Character;

            List<LivingEntity> members = packet.PartyMemberInfos
                .Select(info => character.Map.GetEntity<LivingEntity>(info.EntityType, info.EntityId))
                .Where(entity => entity != null)
                .ToList();

            var owner = members.FirstOrDefault(x => x.EntityType == EntityType.PLAYER) as Player;

            character.Party = new Party(owner ?? client.Character, members);
        }
    }
}