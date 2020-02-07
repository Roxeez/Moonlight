using Microsoft.Extensions.DependencyInjection;
using Moonlight.Core;
using Moonlight.Database.DAL;
using Moonlight.Database.Dto;
using Moonlight.Database.Entities;

namespace Moonlight.Database.Extensions
{
    internal static class ServiceCollectionExtension
    {
        public static void AddDatabaseDependencies(this IServiceCollection services, AppConfig config)
        {
            services.AddTransient<AppConfig>(x => config);
            services.AddTransient<IContextFactory<MoonlightContext>, SqliteContextFactory>();
            services.AddTransient(typeof(IMapper<,>), typeof(MapsterMapper<,>));

            services.AddTransient<IStringRepository<TranslationDto>, CachedStringRepository<Entities.Translation, TranslationDto, MoonlightContext>>();
            services.AddTransient<IRepository<MapDto>, Repository<Map, MapDto, MoonlightContext>>();
            services.AddTransient<IRepository<ItemDto>, Repository<Item, ItemDto, MoonlightContext>>();
            services.AddTransient<IRepository<MonsterDto>, CachedRepository<Monster, MonsterDto, MoonlightContext>>();
            services.AddTransient<IRepository<SkillDto>, Repository<Skill, SkillDto, MoonlightContext>>();
        }
    }
}