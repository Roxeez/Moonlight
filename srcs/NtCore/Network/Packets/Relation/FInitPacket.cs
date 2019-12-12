using System.Collections.Generic;
using NtCore.Game.Relation;
using NtCore.Game.Relation.Impl;

namespace NtCore.Network.Packets.Relation
{
    [PacketInfo("finit", PacketType.Recv)]
    public class FInitPacket : Packet
    {
        public List<IFriend> Friends { get; } = new List<IFriend>();

        public override bool Deserialize(string[] packet)
        {
            if (packet.Length == 1)
            {
                return true;
            }
            
            for (int i = 1; i < packet.Length; i++)
            {
                string[] split = packet[i].Split('|');
                
                Friends.Add(new Friend(int.Parse(split[0]), split[3]));
            }

            return true;
        }
    }
}