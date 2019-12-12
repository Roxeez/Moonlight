using System.Collections.Generic;

namespace NtCore.Network.Packets.Relation
{
    [PacketInfo("finfo", PacketType.Recv)]
    public class FInfoPacket : Packet
    {
        public List<Info> Infos { get; } = new List<Info>();

        public override bool Deserialize(string[] packet)
        {
            foreach (string value in packet)
            {
                string[] split = value.Split('.');
                Infos.Add(new Info(int.Parse(split[0]), split[1] == "1"));
            }

            return true;
        }

        public class Info
        {
            public int Id { get; }
            public bool IsConnected { get; }

            public Info(int id, bool isConnected)
            {
                Id = id;
                IsConnected = isConnected;
            }
        }
    }
}