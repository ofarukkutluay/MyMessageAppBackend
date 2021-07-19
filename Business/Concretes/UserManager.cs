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
using DataAccess.Concretes;
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
            if (resultValidate.IsValid && GetByMail(entity.Email) == null)
            {
                entity.CreateTime = DateTime.Now;
                entity.Status = false;
                _userRepository.Insert(entity);
                return new SuccessResult(Messages.Add(entity.Email));
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

        public List<OperationClaim> GetClaims(User user)
        {
            MongoDbUserOperationClaimDal userOperationClaimDal = new MongoDbUserOperationClaimDal();
            MongoDbOperationClaimDal operationClaimDal = new MongoDbOperationClaimDal();
            List<UserOperationClaim> userOperationClaims = userOperationClaimDal.SearchFor(uoc => uoc.UserId == user.Id);
            List<OperationClaim> operationClaims = new List<OperationClaim>();
            foreach (var uoc in userOperationClaims)
            {
                operationClaims.AddRange(operationClaimDal.SearchFor(oc => oc.Id == uoc.OperationClaimId).ToList());
            }

            return operationClaims;

        }

        public User GetByMail(string email)
        {
            var result = _userRepository.SearchFor(e => e.Email == email).FirstOrDefault();
            return result;
        }
    }
}
