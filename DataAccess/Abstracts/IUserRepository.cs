using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DataAccess.MongoDb;
using Core.Entities.Concretes;

namespace DataAccess.Abstracts
{
    public interface IUserRepository : IMongoDbRepositoryBase<User>
    {
        Person GetPersonByUserId(string userId);
        Person GetPersonByEmail(string email);
        Person GetPersonByMobileNumber(string mobileNumber);
    }
}
