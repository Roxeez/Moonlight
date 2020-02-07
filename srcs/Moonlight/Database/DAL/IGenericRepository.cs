using System.Collections.Generic;

namespace Moonlight.Database.DAL
{
    internal interface IGenericRepository<in TObjectId, TObject> where TObject : class
    {
        IEnumerable<TObject> GetAll();
        TObject Select(TObjectId id);
        TObject Insert(TObject obj);
        IEnumerable<TObject> InsertAll(IEnumerable<TObject> objects);
        void Delete(TObjectId id);

        void Clear();
    }

    internal interface IRepository<TObject> : IGenericRepository<int, TObject> where TObject : class
    {
    }

    internal interface IStringRepository<TObject> : IGenericRepository<string, TObject> where TObject : class
    {
    }
}