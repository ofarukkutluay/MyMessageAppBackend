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
    public class MongoDbUserDal:MongoDbRepositoryBase<User>,IUserRepository
    {
        public Person GetPersonByUserId(string userId)
        {
            Person person = base.GetById(userId);
            return person;
        }

        public Person GetPersonByEmail(string email)
        {
            Person person = base.SearchFor(p => p.Email == email).SingleOrDefault();
            return person;
        }

        public Person GetPersonByMobileNumber(string mobileNumber)
        {
            Person person = base.SearchFor(p => p.MobileNumber == mobileNumber).SingleOrDefault();
            return person;
        }
    }
}
