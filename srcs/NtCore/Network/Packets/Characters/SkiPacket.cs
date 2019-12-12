using System.Collections.Generic;
using System.Linq;

namespace NtCore.Network.Packets.Characters
{
    [PacketInfo("ski", PacketType.Recv)]
    public class SkiPacket : Packet
    {
        public IEnumerable<int> Skills { get; private set; }

        public override bool Deserialize(string[] packet)
        {
            Skills = packet.Select(int.Parse);

            return true;
        }
    }
}