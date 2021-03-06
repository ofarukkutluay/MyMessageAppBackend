using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DataAccess.MongoDb;
using Entities.Concretes;

namespace DataAccess.Abstracts
{
    public interface IMessageRepository : IMongoDbRepositoryBase<Message>
    {
    }
}
