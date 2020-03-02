using Moonlight.Clients;
using Moonlight.Core.Logging;
using Moonlight.Event;
using Moonlight.Event.Characters;
using Moonlight.Game.Entities;
using Moonlight.Game.Factory;
using Moonlight.Game.Maps;
using Moonlight.Packet.Character;

namespace Moonlight.Handlers.Characters
{
    internal class CInfoPacketHandler : PacketHandler<CInfoPacket>
    {
        private readonly IEventManager _eventManager;
        private readonly ILogger _logger;
        private readonly IMapFactory _mapFactory;

        public CInfoPacketHandler(ILogger logger, IMapFactory mapFactory, IEventManager eventManager)
        {
            _logger = logger;
            _mapFactory = mapFactory;
            _eventManager = eventManager;
        }

        protected override void Handle(Client client, CInfoPacket packet)
        {
            if (client.Character == null)
            {
                Miniland miniland = _mapFactory.CreateMiniland();

                miniland.Owner = packet.Name;
                
                client.Character = new Character(_logger, packet.CharacterId, packet.Name, client)
                {
                    Class = packet.Class,
                    Gender = packet.Gender
                };

                _eventManager.Emit(new CharacterInitializeEvent(client)
                {
                    Character = client.Character
                });
            }
        }
    }
}