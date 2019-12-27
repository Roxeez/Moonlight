using System.Collections.Generic;
using System.Linq;
using NtCore.Clients;
using NtCore.Enums;
using NtCore.Extensions;
using NtCore.Game.Entities;
using NtCore.Game.Entities.Impl;
using NtCore.Game.Relation.Impl;
using NtCore.Network.Packets.Relation;

namespace NtCore.Network.Handlers.Relation
{
    public class PInitPacketHandler : PacketHandler<PInitPacket>
    {
        public override void Handle(IClient client, PInitPacket packet)
        {
            var character = client.Character.As<Character>();

            var members = new List<ILivingEntity>();
            foreach (PartyMemberInfo info in packet.PartyMemberInfos)
            {
                var entity = character.Map.GetEntity(info.EntityType, info.EntityId).As<ILivingEntity>();
                if (entity == null)
                {
                    continue;
                }

                members.Add(entity);
            }

            var owner = members.FirstOrDefault(x => x.EntityType == EntityType.PLAYER)?.As<IPlayer>();

            character.Party = new Party(owner ?? client.Character, members);
        }
    }
}