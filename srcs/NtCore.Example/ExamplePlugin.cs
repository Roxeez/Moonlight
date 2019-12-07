using NtCore.API;
using NtCore.API.Plugins;

namespace NtCore.Example
{
    public class ExamplePlugin : Plugin
    {
        public override string Name => "Example";
        public override string Version => "1.0";

        public override void OnEnable()
        {
            NtCoreAPI.GetPluginManager().Register<ExampleListener>(this);
            NtCoreAPI.GetCommandManager().Register<ExampleCommandHandler>(this);
        }
    }
}