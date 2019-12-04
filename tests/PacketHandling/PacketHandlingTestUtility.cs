using NtCore.API.Client;

namespace NtCore.Tests.PacketHandling
{
    public static class PacketHandlingTestUtility
    {
        public static void CreateFakeMap(IClient client)
        {
            // Join map
            client.ReceivePacket("c_map 0 150 1");

            // Add npc
            client.ReceivePacket("in 2 322 2053 36 131 7 100 100 0 0 0 -1 1 0 -1 - 2 -1 0 0 0 0 0 0 0 0");
            client.ReceivePacket("in 2 150 1026 124 63 7 86 47 0 0 0 -1 1 0 -1 - 2 -1 0 0 0 0 0 0 0 0");
            
            // Add monster
            client.ReceivePacket("in 3 24 1874 17 156 2 100 100 0 0 0 -1 1 0 -1 - 2 -1 0 0 0 0 0 0 0 0");
            client.ReceivePacket("in 3 294 874 54 26 2 14 87 0 0 0 -1 1 0 -1 - 2 -1 0 0 0 0 0 0 0 0");
        }
    }
}