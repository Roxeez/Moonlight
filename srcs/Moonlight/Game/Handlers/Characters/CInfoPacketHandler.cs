using Moonlight.Clients;
using Moonlight.Core.Logging;
using Moonlight.Game.Entities;
using Moonlight.Game.Factory;
using Moonlight.Packet.Character;

namespace Moonlight.Game.Handlers.Characters
{
    internal class CInfoPacketHandler : PacketHandler<CInfoPacket>
    {
        private readonly ILogger _logger;
        private readonly IMapFactory _mapFactory;

        public CInfoPacketHandler(ILogger logger, IMapFactory mapFactory)
        {
            _logger = logger;
            _mapFactory = mapFactory;
        }

        protected override void Handle(Client client, CInfoPacket packet)
        {
            client.Character = new Character(packet.CharacterId, packet.Name, client, _mapFactory.CreateMiniland())
            {
                Class = packet.Class,
                Gender = packet.Gender
            };

            _logger.Info("Character initialized");
        }
    }
}