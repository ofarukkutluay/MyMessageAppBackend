using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Business;
using Core.Entities.Concretes;

namespace Business.Abstracts
{
    public interface IUserService : IServiceRepository<User>
    {
    }
}
