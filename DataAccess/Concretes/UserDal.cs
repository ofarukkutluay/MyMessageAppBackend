using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DataAccess.MongoDb;
using Core.Entities.Concretes;
using DataAccess.Abstracts;

namespace DataAccess.Concretes
{
    public class UserDal:MongoDbRepositoryBase<User>,IUserRepository
    {
    }
}
