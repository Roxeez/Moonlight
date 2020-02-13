using System.Data.Entity;

namespace Moonlight.Database.DAL
{
    public interface IContextFactory<out T> where T : DbContext
    {
        T CreateContext();
    }
}