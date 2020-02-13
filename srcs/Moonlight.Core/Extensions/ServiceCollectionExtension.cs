using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Moonlight.Core.Logging;

namespace Moonlight.Core.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddImplementingTypes<T>(this IServiceCollection services)
        {
            IEnumerable<Type> types = typeof(T).Assembly.GetAssignableTypes<T>();
            foreach (Type type in types)
            {
                services.AddTransient(typeof(T), type);
            }
        }

        public static void AddImplementingTypes<T>(this IServiceCollection services, Assembly assembly)
        {
            IEnumerable<Type> types = assembly.GetAssignableTypes<T>();
            foreach (Type type in types)
            {
                services.AddTransient(typeof(T), type);
            }
        }

        public static void AddLogger(this IServiceCollection services)
        {
            services.AddTransient<ILogger, SerilogLogger>();
        }
    }
}