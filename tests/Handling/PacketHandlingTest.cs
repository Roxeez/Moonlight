using Moonlight.Clients;
using Moonlight.Core;
using Moonlight.Game.Entities;
using Moonlight.Game.Maps;
using Moq;

namespace Moonlight.Tests.Handling
{
    public abstract class PacketHandlingTest
    {
        protected MoonlightAPI Moonlight { get; }
        protected PacketHandlingTest()
        {
            var clientMock = new Mock<Client>();

            Moonlight = new MoonlightAPI(new AppConfig
            {
                Database = "../../database.db"
            });

            clientMock.Setup(x => x.SendPacket(It.IsAny<string>())).Callback<string>(x => Moonlight.GetPacketHandlerManager().Handle(clientMock.Object, x));
            clientMock.Setup(x => x.ReceivePacket(It.IsAny<string>())).Callback<string>(x => Moonlight.GetPacketHandlerManager().Handle(clientMock.Object, x));

            Client = clientMock.Object;
            Client.Character = Character = new Character(999, "Moonlight", Client, new Miniland("Miniland", new byte[4096]));
        }

        protected Client Client { get; }
        protected Character Character { get; }
    }
}