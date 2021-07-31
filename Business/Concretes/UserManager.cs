using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstracts;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
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
        
        [SecuredOperation("admin")]
        [CacheAspect]
        [PerformanceAspect(2)]
        public IDataResult<List<User>> GetAll()
        {
            List<User> result = _userRepository.GetAll().ToList();
            if (result.Count == 0)
            {
                return new ErrorDataResult<List<User>>(Messages.ListEmpty);
            }
            return new SuccessDataResult<List<User>>(result, Messages.GetAll);
        }

        [TransactionScopeAspect]
        [ValidationAspect(typeof(UserValidator))]
        public IResult Add(User entity)
        {
            
            if ( GetByMail(entity.Email) == null)
            {
                _userRepository.Insert(entity);
                return new SuccessResult(Messages.Add(entity.Email));
            }

            return new ErrorResult(Messages.UserAlreadyExists);

        }


        [TransactionScopeAspect]
        public IDataResult<User> GetById(string id)
        {
            var result = _userRepository.GetById(id);
            return new SuccessDataResult<User>(result, Messages.GetById(result.Email));
        }

        [SecuredOperation("admin,kullanici")]
        [CacheRemoveAspect("IUserService.Get")]
        [ValidationAspect(typeof(UserValidator))]
        public IResult Update(User entity)
        {
            if (entity.Id != null)
            {
                _userRepository.Update(entity);
                return new SuccessResult(Messages.Update(entity.Id));
            }

            return new ErrorResult(Messages.IdNotFound);
        }

        [SecuredOperation("admin")]
        [CacheRemoveAspect("IUserService.Get")]
        public IResult Delete(User entity)
        {
            if (entity.Id != null)
            {
                _userRepository.Delete(entity);
                return new SuccessResult(Messages.Delete(entity.Id));
            }

            return new ErrorResult(Messages.IdNotFound);
        }

        [TransactionScopeAspect]
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
