using Moonlight.Clients;
using Moonlight.Game.Factory;
using Moonlight.Packet.Map.Miniland;

namespace Moonlight.Game.Handlers.Maps.Minilands
{
    internal class MlObjLstPacketHandler : PacketHandler<MlObjLstPacket>
    {
        private readonly IMinilandObjectFactory _minilandObjectFactory;

        public MlObjLstPacketHandler(IMinilandObjectFactory minilandObjectFactory) => _minilandObjectFactory = minilandObjectFactory;

        protected override void Handle(Client client, MlObjLstPacket packet)
        {
        }
    }
}