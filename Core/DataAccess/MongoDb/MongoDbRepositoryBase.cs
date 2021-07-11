using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Core.Entities.MongoDb;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Core.DataAccess.MongoDb
{
    public class MongoDbRepositoryBase<TEntity> : IMongoDbRepositoryBase<TEntity> where TEntity : EntityBase
    {

        private IMongoDatabase database;
        private IMongoCollection<TEntity> collection;

        public MongoDbRepositoryBase()
        {
            GetDatabase();
            GetCollection();
        }
        public void Insert(TEntity entity)
        {
            entity.Id = ObjectId.GenerateNewId().ToString();
            collection.InsertOne(entity);
            
        }
        public void Update(TEntity entity)
        {
            var filter = Builders<TEntity>.Filter.Eq("Id", entity.Id);

            var updateDefination = new List<UpdateDefinition<TEntity>>();
            foreach (var dataField in typeof(TEntity).GetProperties())
            {
                if (dataField.GetValue(entity)!=null)
                {
                    updateDefination.Add(Builders<TEntity>.Update.Set(dataField.Name, dataField.GetValue(entity)));
                }
                
            }
            var combinedUpdate = Builders<TEntity>.Update.Combine(updateDefination);

            collection.UpdateOne(filter, combinedUpdate);
            
        }
        public void Delete(TEntity entity)
        {
            var filter = Builders<TEntity>.Filter.Eq("Id", entity.Id);
            collection.DeleteOne(filter);
            
        }
        public List<TEntity>
            SearchFor(Expression<Func<TEntity, bool>> predicate)
        {
            return collection
                .AsQueryable<TEntity>()
                    .Where(predicate.Compile())
                        .ToList();
        }
        public List<TEntity> GetAll()
        {
            var result= collection.Find(Builders<TEntity>.Filter.Empty).ToList();

            return result;
        }
        public TEntity GetById(string id)
        {
            var filter = Builders<TEntity>.Filter.Eq("Id", id);
            var result = collection.Find(filter).FirstOrDefault();
            return result;
        }

        //region Private Helper Methods
        private void GetDatabase()
        {
            var client = new MongoClient(GetConnectionString());
            //var server = client.GetServer();
            database = client.GetDatabase(GetDatabaseName());
        }

        private string GetConnectionString()
        {
            /* return ConfigurationManager
                 .AppSettings
                     .Get("MongoDbConnectionString")
                         .Replace("{DB_NAME}", GetDatabaseName()); */
            string connectionUrl = "mongodb+srv://drCmd:gyuayhd3hhDfIeUA@mymessageappdb.dw6rz.mongodb.net/{DB_NAME}?retryWrites=true&w=majority";
            connectionUrl.Replace("{DB_NAME}", GetDatabaseName());
            return connectionUrl;

        }
        private string GetDatabaseName()
        {
            /* return ConfigurationManager
                 .AppSettings
                     .Get("MongoDbDatabaseName"); */
            return "MyMessage";
        }
        private void GetCollection()
        {
            collection = database
                .GetCollection<TEntity>(typeof(TEntity).Name+"s");
        }
        //endregion
    }
}
