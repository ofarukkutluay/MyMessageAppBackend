using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DataAccess.MongoDb;
using Core.Entities.Concretes;

namespace DataAccess.Abstracts
{
    public interface IUserOperationClaimRepository : IMongoDbRepositoryBase<UserOperationClaim>
    {
    }
}
