using System;

namespace NtCore.API.Plugins
{
    [AttributeUsage(AttributeTargets.Class)]
    public class PluginInfo : Attribute
    {
        public string Name { get; set; }
        public string Version { get; set; }
        public bool IsInjected { get; set; }
    }
}