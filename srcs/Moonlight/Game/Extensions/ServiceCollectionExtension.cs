using Microsoft.Extensions.DependencyInjection;
using Moonlight.Game.Factory;
using Moonlight.Game.Factory.Impl;

namespace Moonlight.Game.Extensions
{
    internal static class ServiceCollectionExtension
    {
        public static void AddFactories(this IServiceCollection services)
        {
            services.AddTransient<IMapFactory, MapFactory>();
            services.AddTransient<IEntityFactory, EntityFactory>();
            services.AddTransient<IItemFactory, ItemFactory>();
            services.AddTransient<IMinilandObjectFactory, MinilandObjectFactory>();
            services.AddTransient<ISkillFactory, SkillFactory>();
        }
    }
}