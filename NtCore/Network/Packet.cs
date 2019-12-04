using System;
using System.Reflection;
using NtCore.Logging;

namespace NtCore.Packets
{
    public abstract class Packet : IPacket
    {
        public virtual bool Deserialize(string[] packet)
        {
            Type type = GetType();

            foreach (PropertyInfo propertyInfo in type.GetProperties())
            {
                Index indexAttribute = propertyInfo.GetCustomAttribute<Index>();
                if (indexAttribute == null)
                {
                    Logger.Debug($"[{type.Name} - Deserializer] No index attribute found on property {propertyInfo.Name}");
                    continue;
                }

                int index = indexAttribute.Value;
                if (index >= packet.Length)
                {
                    Logger.Debug($"[{type.Name} - Deserializer] Packet length is smaller than index");
                    continue;
                }

                try
                {
                    propertyInfo.SetValue(this, Parse(packet[index], propertyInfo.PropertyType));
                }
                catch (Exception e)
                {
                    Logger.Error(e.StackTrace);
                    return false;
                }
            }
            return true;
        }

        private static object Parse(string value, Type targetType)
        {
            if (targetType == typeof(bool))
            {
                return value == "1";
            }

            return Convert.ChangeType(value, targetType);
        }
    }
}