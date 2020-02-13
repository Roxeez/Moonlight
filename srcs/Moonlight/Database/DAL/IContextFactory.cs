using System.Data.Entity;

namespace Moonlight.Database.DAL
{
    internal interface IContextFactory<out T> where T : DbContext
    {
        T CreateContext();
    }
}