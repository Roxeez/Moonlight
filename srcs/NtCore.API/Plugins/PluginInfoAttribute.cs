using System;

namespace NtCore.API.Plugins
{
    [AttributeUsage(AttributeTargets.Class)]
    public class PluginInfoAttribute : Attribute
    {
        public bool NeedInjection { get; set; }
    }
}