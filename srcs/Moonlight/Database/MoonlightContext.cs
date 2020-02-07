using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Moonlight.Database.Entities;
using SQLite.CodeFirst;

namespace Moonlight.Database
{
    internal class MoonlightContext : DbContext
    {
        public MoonlightContext(DbConnection connection) : base(connection, true)
        {
        }

        public DbSet<Monster> Monsters { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Map> Maps { get; set; }
        public DbSet<Entities.Translation> Translations { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var initializer = new SqliteCreateDatabaseIfNotExists<MoonlightContext>(modelBuilder);
            System.Data.Entity.Database.SetInitializer(initializer);

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            base.OnModelCreating(modelBuilder);
        }
    }
}