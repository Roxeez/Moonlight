using NtCore.API.Logger;

namespace NtCore.API.Plugins
{
    public class Plugin
    {
        protected readonly INtCore NtCore;
        
        public ILogger Logger { get; }

        public Plugin(INtCore ntCore)
        {
            NtCore = ntCore;
        }
    }
}