﻿using Moonlight.Clients;
using Moonlight.Packet.Map.Miniland.Minigame;

namespace Moonlight.Handlers.Maps.Minilands.Minigames
{
    internal class MlPtPacketHandler : PacketHandler<MlPtPacket>
    {
        protected override void Handle(Client client, MlPtPacket packet)
        {
            client.Character.ProductionPoints = packet.Points;
        }
    }
}