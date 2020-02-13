namespace Moonlight.Packet
{
    public interface IPacket
    {
        string Header { get; set; }
        string Content { get; set; }
    }

    public abstract class Packet : IPacket
    {
        public string Header { get; set; }
        public string Content { get; set; }
    }
}