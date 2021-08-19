using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities.MongoDb;

namespace Core.Entities.Concretes
{
    public class Person : EntityBase
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public long NationaltyId { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public bool Status { get; set; }
        
    }
}
