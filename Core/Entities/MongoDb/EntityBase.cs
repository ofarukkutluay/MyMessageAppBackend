using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;

namespace Core.Entities.MongoDb
{
    public abstract class EntityBase
    {
        [BsonId]
        public string Id { get; set; }
    }
}
