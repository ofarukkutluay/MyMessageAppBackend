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
    public interface IMessageService 
    {
        IDataResult<List<Message>> GetAll();
        IResult Add(Message entity);
        IDataResult<Message> GetById(string id);
        IResult Update(Message entity);
        IResult Delete(Message entity);
        IDataResult<List<Message>> GetBySenderAndReciverAll(string senderId,string reciverId);
    }
}
