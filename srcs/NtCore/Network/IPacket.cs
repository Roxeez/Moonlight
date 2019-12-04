namespace NtCore.Network
{
    public interface IPacket
    {
        bool Deserialize(string[] packet);
    }
}