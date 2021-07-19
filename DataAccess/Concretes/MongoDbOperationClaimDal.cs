using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DataAccess.MongoDb;
using Core.Entities.Concretes;
using DataAccess.Abstracts;
using MongoDB.Driver;

namespace DataAccess.Concretes
{
    public class MongoDbOperationClaimDal : MongoDbRepositoryBase<OperationClaim>,IOperationClaimRepository
    {
    }
}
