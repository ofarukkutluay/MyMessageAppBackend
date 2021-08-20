using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstracts;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Transaction;
using Core.Utilities.IoC;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using DataAccess.Abstracts;
using Entities.Concretes;
using Microsoft.Extensions.DependencyInjection;

namespace Business.Concretes
{
    public class MessageManager : IMessageService
    {
        private IMessageRepository _messageRepository;
        private IUserService _userService;

        public MessageManager(IMessageRepository messageRepository,IUserService userService)
        {
            _messageRepository = messageRepository;
            _userService = userService;
        }

        [SecuredOperation("admin")]
        [CacheAspect]
        public IDataResult<List<Message>> GetAll()
        {
            var result = _messageRepository.GetAll();
            return new SuccessDataResult<List<Message>>(result,Messages.GetAll);
        }

        [SecuredOperation("admin,user")]
        [CacheRemoveAspect("IMessageService.Get")]
        [TransactionScopeAspect]
        public IResult Add(Message entity)
        {
            if (!_userService.GetPersonByUserId(entity.SenderUserId).Data.Status)
            {
                return new ErrorResult(Messages.StatusFalseUser);
            }
            byte[] textHash, textSalt;
            HashingHelper.CreateTextHash(entity.Text,out textHash,out textSalt);
            Message message = new Message()
            {
                Text = entity.Text,
                TextHash = textHash,
                TextSalt = textSalt,
                SenderUserId = entity.SenderUserId,
                ReciverUserId = entity.ReciverUserId,
                SendTime = DateTime.Now
            };
            _messageRepository.Insert(message);
            return new SuccessResult(Messages.Add(entity.Id));
        }

        [SecuredOperation("admin")]
        public IDataResult<Message> GetById(string id)
        {
            var result = _messageRepository.GetById(id);
            return new SuccessDataResult<Message>(result,Messages.GetById(id));
        }

        [SecuredOperation("admin")]
        [CacheRemoveAspect("IMessageService.Get")]
        public IResult Update(Message entity)
        {
            if (entity.Id== null)
            {
                return new ErrorResult(Messages.IdNotFound);
            }
            _messageRepository.Update(entity);
            return new SuccessResult(Messages.Update(entity.Id));
        }

        [SecuredOperation("admin,user")]
        [CacheRemoveAspect("IMessageService.Get")]
        public IResult Delete(Message entity)
        {
            if (entity.Id == null)
            {
                return new ErrorResult(Messages.IdNotFound);
            }
            _messageRepository.Delete(entity);
            return new SuccessResult(Messages.Update(entity.Id));
        }

        [SecuredOperation("admin,user")]
        [PerformanceAspect(2)]
        [CacheAspect]
        public IDataResult<List<Message>> GetBySenderAndReciverAll(string senderId, string reciverId)
        {
            var result = _messageRepository.SearchFor(m => (m.SenderUserId == senderId && m.ReciverUserId == reciverId) || (m.SenderUserId == reciverId && m.ReciverUserId == senderId))
                .OrderByDescending(m => m.SendTime).ToList();
            return new SuccessDataResult<List<Message>>(result, $"{senderId} ve {reciverId} arasıdaki mesajlar tarih sıralamasına göre listelendi");
        }
    }
}
