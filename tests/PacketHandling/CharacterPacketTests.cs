using Moq;
using NFluent;
using NtCore.API.Client;
using NtCore.Game.Entities;
using NtCore.Network;
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
            var mock = new Mock<IClient>();
            
            mock.Setup(x => x.ReceivePacket(It.IsAny<string>())).Callback((string p) =>  _ntCoreManager.PacketManager.Handle(mock.Object, p, PacketType.Recv));
            mock.SetupGet(x => x.Character).Returns(new Character());

            _client = mock.Object;
        }
        
        [Theory]
        [InlineData("stat 2500 2000 1500 1000", 2500, 2000, 1500, 1000)]
        [InlineData("stat 1 2 3 4", 1, 2, 3, 4)]
        public void Stat_Packet_Update_Character_Stats(string packet, int hp, int maxHp, int mp, int maxMp)
        {
            _client.ReceivePacket(packet);

            Check.That(_client.Character.Hp).IsEqualTo(hp);
            Check.That(_client.Character.Mp).IsEqualTo(mp);
            Check.That(_client.Character.MaxHp).IsEqualTo(maxHp);
            Check.That(_client.Character.MaxMp).IsEqualTo(maxMp);
        }
    }
}