using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Moonlight.Core.Extensions
{
    internal static class AssemblyExtension
    {
        public static IEnumerable<Type> GetAssignableTypes<T>(this Assembly assembly)
        {
            return assembly.GetTypes().Where(type => typeof(T).IsAssignableFrom(type)).Where(type => !type.IsAbstract && !type.IsInterface);
        }
    }
}