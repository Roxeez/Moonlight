using System;

namespace NtCore.API.Plugins
{
    /// <summary>
    ///     Attribute used for defining Event handler method
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class Handler : Attribute
    {
    }
}