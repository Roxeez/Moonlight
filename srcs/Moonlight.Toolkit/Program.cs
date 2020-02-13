using System;
using CommandLine;
using Microsoft.Extensions.DependencyInjection;
using Moonlight.Core;
using Moonlight.Core.Extensions;
using Moonlight.Database.Extensions;
using Moonlight.Toolkit.Commands;
using Moonlight.Toolkit.Handlers;

namespace Moonlight.Toolkit
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            IServiceCollection services = new ServiceCollection();

            services.AddSingleton<Manager>();

            services.AddLogger();
            services.AddDatabaseDependencies(new AppConfig
            {
                Database = "output/database.db"
            });

            services.AddImplementingTypes<ICommandHandler>();
            services.AddImplementingTypes<Parsing.Parser>();

            IServiceProvider provider = services.BuildServiceProvider();

            Manager manager = provider.GetService<Manager>();

            ParserResult<object> parserResult = CommandLine.Parser.Default.ParseArguments(args, manager.GetCommandTypes());

            parserResult.MapResult(x => manager.Handle(x as ICommand), errors => false);
        }
    }
}