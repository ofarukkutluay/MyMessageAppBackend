using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Business.Abstracts;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.DependencyResolvers.Autofac;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.Entities.Concretes;
using Core.Utilities.IoC;
using Core.Utilities.Results;
using DataAccess.Abstracts;
using Entities.Concretes;
using Microsoft.Extensions.DependencyInjection;


namespace Business.Concretes
{
    public class UserManager : IUserService
    {
        private IUserRepository _userRepository;
        private IUserOperationClaimRepository _userOperationClaimRepository;
        private IOperationClaimRepository _operationClaimRepository;

        public UserManager(IUserRepository userRepository,IUserOperationClaimRepository userOperationClaimRepository,IOperationClaimRepository operationClaimRepository)
        {
            _userRepository = userRepository;
            _userOperationClaimRepository = userOperationClaimRepository;
            _operationClaimRepository = operationClaimRepository;
        }

        [SecuredOperation("admin")]
        [PerformanceAspect(2)]
        public IDataResult<List<User>> GetAll()
        {
            List<User> result = _userRepository.GetAll();
            if (result.Count == 0)
            {
                return new ErrorDataResult<List<User>>(Messages.ListEmpty);
            }
            return new SuccessDataResult<List<User>>(result, Messages.GetAll);
        }

        //[SecuredOperation("admin")]
        [TransactionScopeAspect]
        [ValidationAspect(typeof(UserValidator))]
        public IResult Add(User entity)
        {
            
            if (GetByMail(entity.Email) == null && GetByMobileNumber(entity.MobileNumber)==null)
            {
                _userRepository.Insert(entity);
                UserOperationClaim userOperationClaim = new UserOperationClaim()
                {
                    OperationClaimId = "6105b352aa76461dcec2f8be",
                    UserId = GetByMail(entity.Email).Id
                };
                _userOperationClaimRepository.Insert(userOperationClaim);
                return new SuccessResult(Messages.Add(entity.Email));
            }
            return new ErrorResult(Messages.UserAlreadyExists);

        }

        [SecuredOperation("admin")]
        [TransactionScopeAspect]
        public IDataResult<User> GetById(string id)
        {
            var result = _userRepository.GetById(id);
            return new SuccessDataResult<User>(result, Messages.GetById(result.Email));
        }

        [SecuredOperation("admin,user")]
        public IDataResult<Person> GetPersonByUserId(string userId)
        {
            var result = _userRepository.GetPersonByUserId(userId);
            return new SuccessDataResult<Person>(result, Messages.GetById(result.Email));
        }

        //[SecuredOperation("admin,user")]
        public IDataResult<Person> GetPersonByEmail(string email)
        {
            var result = _userRepository.GetPersonByEmail(email);
            return new SuccessDataResult<Person>(result, Messages.GetById(result.Id));
        }

        [SecuredOperation("admin,user")]
        public IDataResult<Person> GetPersonByMobileNumber(string mobileNumber)
        {
            var result = _userRepository.GetPersonByMobileNumber(mobileNumber);
            return new SuccessDataResult<Person>(result, Messages.GetById(result.Id));
        }

        [SecuredOperation("admin")]
        [ValidationAspect(typeof(UserValidator))]
        public IResult Update(User entity)
        {
            if (entity.Id != null)
            {
                if (entity.Email != null || entity.MobileNumber != null)
                {
                    if (GetByMail(entity.Email) != null || GetByMobileNumber(entity.MobileNumber) != null)
                    {
                        return new ErrorResult(Messages.UserAlreadyExists);
                    }
                }
                _userRepository.Update(entity);
                return new SuccessResult(Messages.Update(entity.Id));
            }

            return new ErrorResult(Messages.IdNotFound);
        }

        [SecuredOperation("admin,user")]
        public IResult UpdatePerson(Person entity)
        {
            if (entity.Id != null)
            {
                if (entity.Email != null || entity.MobileNumber != null)
                {
                    if (GetByMail(entity.Email) != null || GetByMobileNumber(entity.MobileNumber) != null)
                    {
                        return new ErrorResult(Messages.UserAlreadyExists);
                    }
                }

                _userRepository.Update((User)entity);
                return new SuccessResult(Messages.Update(entity.Id));
            }

            return new ErrorResult(Messages.IdNotFound);
        }

        [SecuredOperation("admin")]
        public IResult ActivateUser(string id,bool status)
        {
            User user = new User()
            {
                Id = id,
                Status = status,
            };
            if (status)
            {
                user.ActivateTime = DateTime.Now;
            }
            else
            {
                user.DeactiveTime = DateTime.Now;
            }
            Update(user);
            return new SuccessResult(Messages.Update(id));
        }

        [SecuredOperation("admin")]
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
            
            List<UserOperationClaim> userOperationClaims = _userOperationClaimRepository.SearchFor(uo =>uo.UserId == user.Id);
            List<OperationClaim> operationClaims = new List<OperationClaim>();
            foreach (UserOperationClaim userOperationClaim in userOperationClaims)
            {
                operationClaims.Add(_operationClaimRepository.GetById(userOperationClaim.OperationClaimId));
            }

            return operationClaims;

        }

        public User GetByMail(string email)
        {
            var result = _userRepository.SearchFor(e => e.Email == email).FirstOrDefault();
            return result;
        }

        public User GetByMobileNumber(string mobileNumber)
        {
            var result = _userRepository.SearchFor(e => e.MobileNumber == mobileNumber).FirstOrDefault();
            return result;
        }


    }
}
