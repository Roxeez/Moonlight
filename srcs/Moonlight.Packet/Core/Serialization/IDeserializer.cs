namespace Moonlight.Packet.Core.Serialization
{
    public interface IDeserializer
    {
        /// <summary>
        ///     Deserialize packet
        /// </summary>
        /// <param name="packet">Raw packet to deserialize</param>
        /// <returns>Packet deserialized</returns>
        IPacket Deserialize(string packet);
    }
}