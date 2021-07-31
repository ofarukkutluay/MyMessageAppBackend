using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstracts;
using Business.BusinessAspects.Autofac;
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

        [SecuredOperation("admin")]
        public IDataResult<List<Message>> GetAll()
        {
            var result = _messageRepository.GetAll();
            return new SuccessDataResult<List<Message>>(result);
        }

        [SecuredOperation("admin,kullanici")]
        public IResult Add(Message entity)
        {
            entity.SendTime = DateTime.Now;
            _messageRepository.Insert(entity);
            return new SuccessResult();
        }

        [SecuredOperation("admin")]
        public IDataResult<Message> GetById(string id)
        {
            var result = _messageRepository.GetById(id);
            return new SuccessDataResult<Message>(result);
        }

        [SecuredOperation("admin")]
        public IResult Update(Message entity)
        {
            _messageRepository.Update(entity);
            return new SuccessResult();
        }

        [SecuredOperation("admin,kullanici")]
        public IResult Delete(Message entity)
        {
            _messageRepository.Delete(entity);
            return new SuccessResult();
        }

        [SecuredOperation("admin,kullanici")]
        public IDataResult<List<Message>> GetBySenderAndReciverAll(string senderId, string reciverId)
        {
            var result = _messageRepository.SearchFor(m => (m.SenderUserId == senderId && m.ReciverUserId == reciverId) || (m.SenderUserId == reciverId && m.ReciverUserId == senderId))
                .OrderByDescending(m => m.SendTime).ToList();
            return new SuccessDataResult<List<Message>>(result, $"{senderId} ve {reciverId} arasıdaki mesajlar tarih sıralamasına göre listelendi");
        }
    }
}
