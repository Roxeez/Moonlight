using System;
using System.Collections.Generic;
using Moonlight.Core.Enums;
using Moonlight.Packet.Character.Inventory;
using Moonlight.Utility.Conversion;
using Moonlight.Utility.Conversion.Converters;

namespace Moonlight.Packet.Core.Converters
{
    internal class InvPacketConverter : Converter<InvPacket>
    {
        protected override InvPacket ToObject(string value, Type type, IConversionFactory factory)
        {
            string[] split = value.Split(' ');

            var subs = new List<IvnSubPacket>();
            var bagType = (BagType)Convert.ToInt32(split[0]);

            for (int i = 1; i < split.Length; i++)
            {
                string[] itemData = split[i].Split('.');

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

                subs.Add(sub);
            }

            return new InvPacket
            {
                BagType = bagType,
                SubPackets = subs
            };
        }

        protected override string ToString(InvPacket value, Type type, IConversionFactory factory) => throw new NotImplementedException();
    }
}