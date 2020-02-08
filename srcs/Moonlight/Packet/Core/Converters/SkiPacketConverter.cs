using System;
using System.Collections.Generic;
using Moonlight.Game.Factory;
using Moonlight.Game.Handlers.Characters;
using Moonlight.Packet.Character;
using Moonlight.Utility.Conversion;
using Moonlight.Utility.Conversion.Converters;

namespace Moonlight.Packet.Core.Converters
{
    internal class SkiPacketConverter : Converter<SkiPacket>
    {
        protected override SkiPacket ToObject(string value, Type type, IConversionFactory factory)
        {
            var skills = new List<int>();
            
            string[] split = value.Split(' ');
            foreach (string entry in split)
            {
                skills.Add(Convert.ToInt32(entry));
            }

            return new SkiPacket
            {
                Skills = skills
            };
        }

        protected override string ToString(SkiPacket value, Type type, IConversionFactory factory) => throw new NotImplementedException();
    }
}