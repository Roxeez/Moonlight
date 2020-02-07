using System.Linq;

namespace Moonlight.Packet.Core
{
    internal class PacketReader : IPacketReader
    {
        public PacketOutput Read(string packet)
        {
            string[] split = packet.Trim().Split(' ');

            string header = split[0];
            string content = split.Length > 1 ? string.Join(" ", split.Skip(1)) : string.Empty;

            return new PacketOutput(header, content);
        }
    }
}