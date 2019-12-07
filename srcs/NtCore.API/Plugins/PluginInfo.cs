using System;

namespace NtCore.API.Plugins
{
    [AttributeUsage(AttributeTargets.Class)]
    public class PluginInfo : Attribute
    {
        public bool NeedInjection { get; set; }
    }
}