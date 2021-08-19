using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstracts;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Core.Entities.Concretes;
using Core.Utilities.Results;
using DataAccess.Abstracts;

namespace Business.Concretes
{
    public class UserOperationClaimManager:IUserOperationClaimService
    {
        private IUserOperationClaimRepository _userOperationClaimRepository;

        public UserOperationClaimManager(IUserOperationClaimRepository userOperationClaimRepository)
        {
            _userOperationClaimRepository = userOperationClaimRepository;
        }

        [SecuredOperation("admin")]
        public IDataResult<List<UserOperationClaim>> GetAll()
        {
            var result = _userOperationClaimRepository.GetAll();
            return new SuccessDataResult<List<UserOperationClaim>>(result, Messages.GetAll);
        }

        [SecuredOperation("admin")]
        public IResult Add(UserOperationClaim entity)
        {
            _userOperationClaimRepository.Insert(entity);
            return new SuccessResult(Messages.Add(entity.ToString()));
        }

        [SecuredOperation("admin")]
        public IDataResult<UserOperationClaim> GetById(string id)
        {
            var result = _userOperationClaimRepository.GetById(id);
            return new SuccessDataResult<UserOperationClaim>(result, Messages.GetById(id));
        }

        [SecuredOperation("admin")]
        public IResult Update(UserOperationClaim entity)
        {
            if (entity.Id!=null)
            {
                _userOperationClaimRepository.Update(entity);
                return new SuccessResult(Messages.Update(entity.Id));
            }

            return new ErrorResult(Messages.IdNotFound);
        }

        [SecuredOperation("admin")]
        public IResult Delete(UserOperationClaim entity)
        {
            if (entity.Id!=null)
            {
                _userOperationClaimRepository.Delete(entity);
                return new SuccessResult(Messages.Delete(entity.Id));
            }

            return new ErrorResult(Messages.IdNotFound);
        }

        [SecuredOperation("admin")]
        public IDataResult<List<UserOperationClaim>> GetByUserId(string userId)
        {
            var result = _userOperationClaimRepository.SearchFor(uoc => uoc.UserId == userId);
            return new SuccessDataResult<List<UserOperationClaim>>(result,Messages.GetById(userId));
        }

        //Sadece auth için kullanım sağlanır 
        public List<UserOperationClaim> ClaimForGetByUserId(string userId)
        {
            var result = _userOperationClaimRepository.SearchFor(uoc => uoc.UserId == userId);
            return result;
        }
    }
}
