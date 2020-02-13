using System;
using System.Linq;
using Moonlight.Conversion;
using Moonlight.Conversion.Converters;
using Moonlight.Core.Enums.Game;
using Moonlight.Packet.Map;

namespace Moonlight.Packet.Core.Converters
{
    public class InPacketConverter : Converter<InPacket>
    {
        protected override InPacket ToObject(string value, Type type, IConversionFactory factory)
        {
            string[] split = value.Split(' ');

            var entityType = (EntityType)Convert.ToInt32(split[0]);
            int startIndex = entityType == EntityType.PLAYER ? 3 : 2;

            var packet = new InPacket
            {
                EntityType = entityType,
                Name = entityType == EntityType.PLAYER ? split[1] : string.Empty,
                Vnum = entityType != EntityType.PLAYER ? Convert.ToInt32(split[1]) : 0,
                EntityId = Convert.ToInt32(split[startIndex]),
                PositionX = Convert.ToInt16(split[startIndex + 1]),
                PositionY = Convert.ToInt16(split[startIndex + 2]),
                Direction = entityType != EntityType.GROUND_ITEM ? Convert.ToByte(split[startIndex + 3]) : (byte)0
            };

            string content = string.Join(" ", split.Skip(startIndex + (entityType == EntityType.GROUND_ITEM ? 3 : 4)));

            switch (entityType)
            {
                case EntityType.MONSTER:
                case EntityType.NPC:
                    packet.NpcSubPacket = (InNpcSubPacket)factory.ToObject(content, typeof(InNpcSubPacket));
                    break;
                case EntityType.PLAYER:
                    packet.PlayerSubPacket = (InPlayerSubPacket)factory.ToObject(content, typeof(InPlayerSubPacket));
                    break;
                case EntityType.GROUND_ITEM:
                    packet.DropSubPacket = (InDropSubPacket)factory.ToObject(content, typeof(InDropSubPacket));
                    break;
                default:
                    throw new InvalidOperationException();
            }

            return packet;
        }

        protected override string ToString(InPacket value, Type type, IConversionFactory factory) => throw new NotImplementedException();
    }
}