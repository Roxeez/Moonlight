using System.Collections.Generic;
using System.Linq;

namespace NtCore.Network.Packets.Maps
{
    [PacketInfo("mltobj", PacketType.Recv)]
    public class MltObjPacket : Packet
    {
        public List<MltObjSubPacket> MinilandObjects { get; set; }

        public override bool Deserialize(string[] packet)
        {
            MinilandObjects = new List<MltObjSubPacket>();

            foreach (string value in packet)
            {
                string[] split = value.Split('.');

                MinilandObjects.Add(new MltObjSubPacket
                {
                    Vnum = int.Parse(split[0]),
                    Id = int.Parse(split[1]),
                    Position = new Position(short.Parse(split[2]), short.Parse(split[3]))
                });
            }

            return true;
        }
    }

    public class MltObjSubPacket
    {
        public int Vnum { get; set; }
        public int Id { get; set; }
        public Position Position { get; set; }
    }
}