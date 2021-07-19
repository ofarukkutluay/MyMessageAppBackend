﻿using System;
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
            var result = _messageRepository.GetAll();
            return new SuccessDataResult<List<Message>>(result);
        }

        public IResult Add(Message entity)
        {
            entity.SendTime = DateTime.Now;
            _messageRepository.Insert(entity);
            return new SuccessResult();
        }

        public IDataResult<Message> GetById(string id)
        {
            var result = _messageRepository.GetById(id);
            return new SuccessDataResult<Message>(result);
        }

        public IResult Update(Message entity)
        {
            _messageRepository.Update(entity);
            return new SuccessResult();
        }

        public IResult Delete(Message entity)
        {
            _messageRepository.Delete(entity);
            return new SuccessResult();
        }
    }
}
