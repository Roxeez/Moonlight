﻿using System.Collections.Generic;
using System.Linq;
using Moq;
using NFluent;
using NtCore.API;
using NtCore.API.Client;
using NtCore.API.Game.Maps;
using NtCore.Extensions;
using NtCore.Game.Entities;
using NtCore.Game.Maps;
using NtCore.Network;
using NtCore.Tests.Extensions;
using Xunit;
using Xunit.Sdk;

namespace NtCore.Tests.PacketHandling
{
    public class MinilandPacketTests
    {
        private readonly IClient _client;

        public MinilandPacketTests()
        {
            var ntCore = new NtCore();
            var mock = new Mock<IClient>();
            
            mock.Setup(x => x.ReceivePacket(It.IsAny<string>())).Callback((string p) =>  ntCore.PacketManager.Handle(mock.Object, p, PacketType.Recv));
            mock.SetupGet(x => x.Character).Returns(new Character());
            
            _client = mock.Object;
        }
        
        [Fact]
        public void CMap_Packet_Change_Character_Map_To_Miniland()
        {
            _client.ReceivePacket("c_map 0 20001 1");

            Check.That(_client.Character.Map).InheritsFrom<IMiniland>();
        }
        
        [Theory]
        [InlineData("mlinfobr 3800 Testastos 2 2 10 Test^Message^Of^Miniland", "Testastos", 2, 2, "Test Message Of Miniland")]
        [InlineData("mlinfobr 3800 Roxeez 10 1254 25 Welcome", "Roxeez", 10, 1254, "Welcome")]
        public void MlInfoBr_Packet_Set_Miniland_Information(string packet, string owner, int visitor, int totalVisitor, string message)
        {
            _client.CreateMinilandMock();
            
            _client.ReceivePacket(packet);

            IMiniland miniland = _client.Character.Map.As<IMiniland>();

            Check.That(miniland).IsNotNull();
            Check.That(miniland.Owner).IsEqualTo(owner);
            Check.That(miniland.Visitor).IsEqualTo(visitor);
            Check.That(miniland.TotalVisitor).IsEqualTo(totalVisitor);
            Check.That(message).IsEqualTo(message);
        }

        [Fact]
        public void MltObj_Packet_Add_MinilandObject_To_Miniland()
        {
            _client.CreateMinilandMock();
            
            _client.ReceivePacket("mltobj 3250.1.15.25 3285.2.25.65 1285.3.65.87");

            IMinilandObject[] objects = Utility.CreateDummyMinilandObjects();

            IMiniland miniland = _client.Character.Map.As<IMiniland>();

            Check.That(miniland).IsNotNull();
            Check.That(miniland.MinilandObjects).CountIs(3);
            Check.That(miniland.MinilandObjects).ContainsExactly(objects);
        }
    }
}