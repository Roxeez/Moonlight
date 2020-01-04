using System;
using Microsoft.Win32;
using NtCore.Core;

namespace NtCore.Services.Registry
{
    public class RegistryReader : IRegistryReader
    {
        public Optional<T> GetValue<T>(RegistryKey parent, string path, string name)
        {
            try
            {
                using (RegistryKey key = parent.OpenSubKey(path))
                {
                    if (key == null)
                    {
                        return Optional.Empty<T>();
                    }

                    var value = (T)key.GetValue(name);
                    return Optional.OfNullable(value);
                }
            }
            catch (Exception e)
            {
                return Optional.Empty<T>();
            }
        }
    }
}