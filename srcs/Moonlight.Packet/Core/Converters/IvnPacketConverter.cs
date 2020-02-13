using System;
using Moonlight.Conversion;
using Moonlight.Conversion.Converters;
using Moonlight.Core.Enums.Game;
using Moonlight.Packet.Character.Inventory;

namespace Moonlight.Packet.Core.Converters
{
    public class IvnPacketConverter : Converter<IvnPacket>
    {
        protected override IvnPacket ToObject(string value, Type type, IConversionFactory factory)
        {
            string[] values = value.Split(' ');
            string[] itemData = values[1].Split('.');

            var bagType = (BagType)Convert.ToInt32(values[0]);

            int slot = Convert.ToInt32(itemData[0]);
            int vnum = Convert.ToInt32(itemData[1]);

            var sub = new IvnSubPacket
            {
                Slot = slot,
                VNum = vnum
            };

            if (bagType == BagType.EQUIPMENT)
            {
                short rarity = Convert.ToInt16(itemData[2]);
                byte upgrade = Convert.ToByte(itemData[3]);

                sub.RareAmount = rarity;
                sub.UpgradeDesign = upgrade;
            }
            else
            {
                int amount = Convert.ToInt32(itemData[2]);
                sub.RareAmount = amount;
            }

            return new IvnPacket
            {
                BagType = bagType,
                SubPacket = sub
            };
        }

        protected override string ToString(IvnPacket value, Type type, IConversionFactory factory) => throw new NotImplementedException();
    }
}