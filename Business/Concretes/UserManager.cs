using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstracts;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Entities.Concretes;
using Core.Utilities.Results;
using DataAccess.Abstracts;
using FluentValidation;

namespace Business.Concretes
{
    public class UserManager : IUserService
    {
        private IUserRepository _userRepository;

        public UserManager(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IDataResult<List<User>> GetAll()
        {
            List<User> result = _userRepository.GetAll().ToList();
            return new SuccessDataResult<List<User>>(result, Messages.GetAll);
        }

        
        public IResult Add(User entity)
        {
            var resultValidate = new UserValidator().Validate(entity);
            if (resultValidate.IsValid)
            {
                if (_userRepository.SearchFor(e => e.Email == entity.Email) == null)
                {
                    entity.CreateTime = DateTime.Now;
                    entity.Status = false;
                    _userRepository.Insert(entity);
                    return new SuccessResult(Messages.Add(entity.Email));
                }
                return new ErrorResult("Aynı emailde kayıt bulunmaktadır.");
            }

            return new ErrorResult(resultValidate.ToString("~"));

        }

        public IDataResult<User> GetById(string id)
        {
            var result = _userRepository.GetById(id);
            return new SuccessDataResult<User>(result, Messages.GetById(result.Email));
        }

        public IResult Update(User entity)
        {
            if (entity.Id != null)
            {
                _userRepository.Update(entity);
                return new SuccessResult(Messages.Update(entity.Email));
            }

            return new ErrorResult("Güncelleme için id gereklidir!");
        }

        public IResult Delete(User entity)
        {
            if (entity.Id != null)
            {
                _userRepository.Delete(entity);
                return new SuccessResult(Messages.Delete(entity.Email));
            }

            return new ErrorResult("Kullanıcıyı silmek için id gereklidir");
        }
    }
}
