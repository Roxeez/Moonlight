namespace Moonlight.Packet
{
    internal class CommandPacket : Packet
    {
        public string Name { get; set; }
        public string[] Arguments { get; set; }
    }
}