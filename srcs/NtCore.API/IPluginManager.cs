using NtCore.API.Plugins;

namespace NtCore.API
{
    public interface IPluginManager
    {
        void Register(params IListener[] listeners);
    }
}