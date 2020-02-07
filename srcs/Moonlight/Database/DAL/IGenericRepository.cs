using System.Collections.Generic;

namespace Moonlight.Database.DAL
{
    internal interface IGenericRepository<in TObjectId, TObject> where TObject : class
    {
        IEnumerable<TObject> GetAll();
        TObject Find(TObjectId id);
        TObject Save(TObject obj);
        IEnumerable<TObject> SaveAll(IEnumerable<TObject> objects);
        void Delete(TObjectId id);
    }

    internal interface IRepository<TObject> : IGenericRepository<int, TObject> where TObject : class
    {
    }

    internal interface IStringRepository<TObject> : IGenericRepository<string, TObject> where TObject : class
    {
    }
}