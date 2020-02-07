namespace Moonlight.Packet.Core
{
    internal struct PacketOutput
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