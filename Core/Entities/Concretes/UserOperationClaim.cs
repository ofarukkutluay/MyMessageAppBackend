using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities.MongoDb;

namespace Core.Entities.Concretes
{
    public class UserOperationClaim : EntityBase, IEntity
    {
        public string UserId { get; set; }
        public string OperationClaimId { get; set; }
    }
}
