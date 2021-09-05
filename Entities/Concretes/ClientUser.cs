using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;
using Core.Entities.MongoDb;

namespace Entities.Concretes
{
    public class ClientUser : EntityBase,IEntity
    {
        public string ClientId { get; set; }
        public string UserEmail { get; set; }

    }
}
