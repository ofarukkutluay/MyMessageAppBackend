using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Business;
using Core.Utilities.Results;
using Entities.Concretes;

namespace Business.Abstracts
{
    public interface IMessageService : IServiceRepository<Message>
    {
        IDataResult<List<Message>> GetBySenderAndReciverAll(string senderId,string reciverId);
    }
}
