using System;

namespace NtCore.Commands
{
    /// <summary>
    ///     Attribute used for defining a method as command
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class CommandAttribute : Attribute
    {
        public CommandAttribute(string name) => Name = name;

        /// <summary>
        ///     Name of the command
        /// </summary>
        public string Name { get; }
    }
}