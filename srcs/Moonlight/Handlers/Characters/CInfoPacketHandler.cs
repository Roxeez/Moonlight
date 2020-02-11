using Moonlight.Clients;
using Moonlight.Core.Logging;
using Moonlight.Game.Entities;
using Moonlight.Game.Factory;
using Moonlight.Packet.Character;

namespace Moonlight.Handlers.Characters
{
    internal class CInfoPacketHandler : PacketHandler<CInfoPacket>
    {
        private readonly IMapFactory _mapFactory;

        public CInfoPacketHandler(IMapFactory mapFactory)
        {
            _mapFactory = mapFactory;
        }

        protected override void Handle(Client client, CInfoPacket packet)
        {
            if (client.Character == null)
            {
                client.Character = new Character(packet.CharacterId, packet.Name, client, _mapFactory.CreateMiniland())
                {
                    Class = packet.Class,
                    Gender = packet.Gender
                };
            }
        }
    }
}