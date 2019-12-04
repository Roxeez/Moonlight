namespace NtCore.Packets
{
    public interface IPacket
    {
        bool Deserialize(string[] packet);
    }
}