using System.Collections.Generic;

namespace NtCore.Network.Packets.Relation
{
    [PacketInfo("finit", PacketType.Recv)]
    public class FInitPacket : Packet
    {
        public List<FriendInfo> Friends { get; } = new List<FriendInfo>();

        public override bool Deserialize(string[] packet)
        {
            if (packet.Length == 1)
            {
                return true;
            }
            
            foreach (string value in packet)
            {
                string[] split = value.Split('|');
                bool isConnected = split[2] == "1";
                
                Friends.Add(new FriendInfo(int.Parse(split[0]), split[3], isConnected));
            }

            return true;
        }

        public class FriendInfo
        {
            public int Id { get; }
            public string Name { get; }
            public bool IsConnected { get; }

            public FriendInfo(int id, string name, bool isConnected)
            {
                Id = id;
                Name = name;
                IsConnected = isConnected;
            }
        }
    }
}