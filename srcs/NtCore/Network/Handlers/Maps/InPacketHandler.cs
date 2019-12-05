using NtCore.API.Client;
using NtCore.API.Enums;
using NtCore.Extensions;
using NtCore.Game.Entities;
using NtCore.Game.Maps;
using NtCore.Network.Packets.Maps;

namespace NtCore.Network.Handlers.Maps
{
    public class InPacketHandler : PacketHandler<InPacket>
    {
        public override void Handle(IClient client, InPacket packet)
        {
            var map = client.Character.Map.As<Map>();
            if (map == null)
            {
                return;
            }
            
            switch (packet.EntityType)
            {
                case EntityType.Npc:
                    map.AddNpc(new Npc
                    {
                        Id = packet.Id,
                        Vnum = packet.Vnum,
                        Position = packet.Position,
                        HpPercentage = packet.HpPercentage,
                        MpPercentage = packet.MpPercentage
                    });
                    break;
                case EntityType.Monster:
                    map.AddMonster(new Monster
                    {
                        Id = packet.Id,
                        Vnum = packet.Vnum,
                        Position = packet.Position,
                        HpPercentage = packet.HpPercentage,
                        MpPercentage = packet.MpPercentage
                    });
                    break;
                case EntityType.Drop:
                    map.AddDrop(new Drop
                    {
                        Id = packet.Id,
                        Vnum = packet.Vnum,
                        Position = packet.Position,
                        Amount = packet.Amount
                    });
                    break;
            }
        }
    }
}