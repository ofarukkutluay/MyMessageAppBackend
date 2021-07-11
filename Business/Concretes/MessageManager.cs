using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstracts;
using Core.Utilities.Results;
using DataAccess.Abstracts;
using Entities.Concretes;

namespace Business.Concretes
{
    public class MessageManager : IMessageService
    {
        private IMessageRepository _messageRepository;

        public MessageManager(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }

        public IDataResult<List<Message>> GetAll()
        {
            throw new NotImplementedException();
        }

        public IResult Add(Message entity)
        {
            throw new NotImplementedException();
        }

        public IDataResult<Message> GetById(long id)
        {
            throw new NotImplementedException();
        }

        public IResult Update(Message entity)
        {
            throw new NotImplementedException();
        }

        public IResult Delete(Message entity)
        {
            throw new NotImplementedException();
        }
    }
}
