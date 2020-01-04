using Microsoft.Win32;
using NtCore.Core;

namespace NtCore.Services.Registry
{
    public interface IRegistryReader
    {
        Optional<T> GetValue<T>(RegistryKey parent, string path, string name);
    }
}