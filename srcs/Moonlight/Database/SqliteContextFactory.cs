using System.Data.SQLite;
using Moonlight.Core;
using Moonlight.Database.DAL;

namespace Moonlight.Database
{
    internal class SqliteContextFactory : IContextFactory<MoonlightContext>
    {
        private readonly AppConfig _appConfig;

        public SqliteContextFactory(AppConfig appConfig) => _appConfig = appConfig;

        public MoonlightContext CreateContext()
        {
            var connection = new SQLiteConnection
            {
                ConnectionString = $"Data Source={_appConfig.Database}"
            };

            return new MoonlightContext(connection);
        }
    }
}