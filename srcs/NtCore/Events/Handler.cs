using System;

namespace NtCore.Events
{
    /// <summary>
    ///     Attribute used for defining Event handler method
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class Handler : Attribute
    {
    }
}