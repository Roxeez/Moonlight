using System;

namespace NtCore.API.Commands
{
    [AttributeUsage(AttributeTargets.Method)]
    public class CommandAttribute : Attribute
    {
        public CommandAttribute(string name) => Name = name;

        public string Name { get; }
    }
}