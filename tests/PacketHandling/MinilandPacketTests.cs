using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NFluent;
using NtCore.API.Game.Maps;
using NtCore.Clients;
using NtCore.Extensions;
using NtCore.Game.Entities.Impl;
using NtCore.Game.Maps;
using NtCore.Game.Maps.Impl;
using NtCore.Network;
using NtCore.Tests.Utility;
using Xunit;

namespace NtCore.Tests.PacketHandling
{
    public class MinilandPacketTests
    {
        public MinilandPacketTests()
        {
            var mock = new Mock<IClient>();

            mock.Setup(x => x.ReceivePacket(It.IsAny<string>()))
                .Callback((string p) => NtCoreAPI.Instance.GetPacketManager().Handle(mock.Object, p, PacketType.Recv));
            mock.SetupGet(x => x.Character).Returns(new Character(mock.Object));

            _client = mock.Object;
        }

        private readonly IClient _client;

        [Theory]
        [InlineData("mlinfobr 3800 Testastos 2 2 10 Test^Message^Of^Miniland", "Testastos", 2, 2,
            "Test Message Of Miniland")]
        [InlineData("mlinfobr 3800 Roxeez 10 1254 25 Welcome", "Roxeez", 10, 1254, "Welcome")]
        public void MlInfoBr_Packet_Set_Miniland_Information(string packet, string owner, int visitor, int totalVisitor,
            string message)
        {
            Map fakeMap = new MapBuilder().AsMiniland().Create();

            fakeMap.AddEntity(_client.Character);

            _client.ReceivePacket(packet);

            var miniland = _client.Character.Map.As<IMiniland>();

            Check.That(miniland).IsNotNull();
            Check.That(miniland.Owner).IsEqualTo(owner);
            Check.That(miniland.Visitor).IsEqualTo(visitor);
            Check.That(miniland.TotalVisitor).IsEqualTo(totalVisitor);
            Check.That(message).IsEqualTo(message);
        }

        [Fact]
        public void CMap_Packet_Change_Character_Map_To_Miniland()
        {
            _client.ReceivePacket("c_map 0 20001 1");

            Check.That(_client.Character.Map).InheritsFrom<IMiniland>();
        }

        [Fact]
        public void MltObj_Packet_Add_MinilandObject_To_Miniland()
        {
            var objects = new List<IMinilandObject>()
            {
                new MinilandObject
                {
                    Id = 1,
                    Vnum = 3250,
                    Position = new Position(15, 25)
                },
                new MinilandObject
                {
                    Id = 2,
                    Vnum = 3285,
                    Position = new Position(25, 65)
                },
                new MinilandObject
                {
                    Id = 3,
                    Vnum = 1285,
                    Position = new Position(65, 87)
                }
            };
            
            Map fakeMap = new MapBuilder().AsMiniland().Create();

            fakeMap.AddEntity(_client.Character);

            _client.ReceivePacket("mltobj 3250.1.15.25 3285.2.25.65 1285.3.65.87");

            var miniland = _client.Character.Map.As<IMiniland>();

            Check.That(miniland).IsNotNull();
            Check.That(miniland.MinilandObjects).CountIs(3);
            Check.That(miniland.MinilandObjects).ContainsExactly(objects);
        }
    }
}