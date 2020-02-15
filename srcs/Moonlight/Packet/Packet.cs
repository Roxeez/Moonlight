namespace Moonlight.Packet
{
    internal interface IPacket
    {
        string Header { get; set; }
        string Content { get; set; }
    }

    internal abstract class Packet : IPacket
    {
        public string Header { get; set; }
        public string Content { get; set; }
    }
}