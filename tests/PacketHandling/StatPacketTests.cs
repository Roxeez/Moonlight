using NFluent;
using NtCore.API.Client;
using Xunit;

namespace NtCore.Tests.PacketHandling
{
    public class StatPacketTests
    {
        private readonly NtCoreManager _ntCoreManager;
        private readonly IClient _client;

        public StatPacketTests()
        {
            _ntCoreManager = new NtCoreManager();
            _client = _ntCoreManager.CreateClient();
        }
        
        [Fact]
        public void Stat_Packet_Update_Character_Stats()
        {
            _client.ReceivePacket("stat 2500 2000 1500 1000");

            Check.That(_client.Character.Hp).Equals(2500);
            Check.That(_client.Character.MaxHp).Equals(2000);
            Check.That(_client.Character.Mp).Equals(1500);
            Check.That(_client.Character.MaxMp).Equals(1000);
            
            _client.ReceivePacket("stat 1 2 3 4");
            
            Check.That(_client.Character.Hp).Equals(1);
            Check.That(_client.Character.MaxHp).Equals(2);
            Check.That(_client.Character.Mp).Equals(3);
            Check.That(_client.Character.MaxMp).Equals(4);
        }
    }
}