using System.Collections.Generic;
using System.Linq;
using NtCore.Enums;

namespace NtCore.Network.Packets.Relation
{
    [PacketInfo("pinit", PacketType.Recv)]
    public class PInitPacket : Packet
    {
        public List<PartyMemberInfo> PartyMemberInfos { get; } = new List<PartyMemberInfo>();

        public override bool Deserialize(string[] packet)
        {
            foreach (string value in packet.Skip(1))
            {
                string[] split = value.Split('|');

                var entityType = (EntityType)short.Parse(split[0]);
                int entityId = int.Parse(split[1]);
                
                PartyMemberInfos.Add(new PartyMemberInfo(entityType, entityId));
            }

            return true;
        }
    }
    
    public struct PartyMemberInfo
    {
        public EntityType EntityType { get; }
        public int EntityId { get; }

        public PartyMemberInfo(EntityType entityType, int entityId)
        {
            EntityType = entityType;
            EntityId = entityId;
        }
    }
}