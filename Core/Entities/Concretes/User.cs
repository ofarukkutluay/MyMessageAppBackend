using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities.MongoDb;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace Core.Entities.Concretes
{
    public class User :  Person, IEntity
    {
        public User()
        {
            CreateTime = DateTime.Now;
            Status = false;
        }
        //public string FirstName { get; set; }
        //public string LastName { get; set; }
        //public string Email { get; set; }
        //public long NationaltyId { get; set; }
        //public long MobileNumber { get; set; }
        //public DateTime? DateOfBirth { get; set; }
        //public bool Status { get; set; }
        public DateTime? CreateTime { get; set; }
        public DateTime? ActivateTime { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}
