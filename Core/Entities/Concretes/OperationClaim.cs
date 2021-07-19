using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities.MongoDb;

namespace Core.Entities.Concretes
{
    public class OperationClaim : EntityBase, IEntity
    {
        public string Name { get; set; }

    }
}
