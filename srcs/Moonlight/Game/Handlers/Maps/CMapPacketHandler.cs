using Moonlight.Clients;
using Moonlight.Core.Logging;
using Moonlight.Game.Factory;
using Moonlight.Game.Maps;
using Moonlight.Packet.Map;

namespace Moonlight.Game.Handlers.Maps
{
    internal class CMapPacketHandler : PacketHandler<CMapPacket>
    {
        private readonly ILogger _logger;
        private readonly IMapFactory _mapFactory;

        public CMapPacketHandler(ILogger logger, IMapFactory mapFactory)
        {
            _logger = logger;
            _mapFactory = mapFactory;
        }

        protected override void Handle(Client client, CMapPacket packet)
        {
            if (!packet.IsBaseMap)
            {
                _logger.Debug("Is base map");
                return;
            }

            Map map = _mapFactory.CreateMap(packet.MapId);
            if (client.Character.Map?.Id == map.Id)
            {
                return;
            }

            map.AddEntity(client.Character);
            _logger.Debug($"Map changed to {map.Id}");
        }
    }
}