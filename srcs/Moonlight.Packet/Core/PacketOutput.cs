namespace Moonlight.Packet.Core
{
    public struct PacketOutput
    {
        public PacketOutput(string header, string content)
        {
            Header = header;
            Content = content;
        }

        public string Header { get; }
        public string Content { get; }
    }
}