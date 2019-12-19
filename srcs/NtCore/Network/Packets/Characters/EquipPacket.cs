using System.Collections.Generic;
using NtCore.Enums;

namespace NtCore.Network.Packets.Characters
{
    [PacketInfo("equip", PacketType.Recv)]
    public class EquipPacket : Packet
    {
        public Dictionary<EquipmentType, EquipSubPacket> EquipSubPackets { get; } = new Dictionary<EquipmentType, EquipSubPacket>();

        public override bool Deserialize(string[] packet)
        {
            foreach (string value in packet)
            {
                string[] split = value.Split('.');

                if (split.Length < 4)
                {
                    continue;
                }
                
                var equipmentType = (EquipmentType)byte.Parse(split[0]);
                var subPacket = new EquipSubPacket
                {
                    Vnum = int.Parse(split[1]),
                    Rarity = byte.Parse(split[2]),
                    Upgrade = byte.Parse(split[3])
                };

                EquipSubPackets[equipmentType] = subPacket;
            }

            return true;
        }
    }

    public class EquipSubPacket
    {
        public int Vnum { get; set; }
        
        public byte Rarity { get; set; }
        
        public byte Upgrade { get; set; }
    }
}