using Core.Entities.MongoDb;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.DataAccess.MongoDb
{
    public interface IMongoDbRepositoryBase<TEntity> where TEntity : EntityBase
    {
        void Insert(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        List<TEntity> SearchFor(Expression<Func<TEntity, bool>> predicate);
        List<TEntity> GetAll();
        TEntity GetById(string id);
    }
}
