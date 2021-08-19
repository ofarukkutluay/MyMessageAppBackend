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
    public class OperationClaimManager : IOperationClaimService
    {
        private IOperationClaimRepository _operationClaimRepository;
        public OperationClaimManager(IOperationClaimRepository operationClaimRepository)
        {
            _operationClaimRepository = operationClaimRepository;
        }

        [SecuredOperation("admin")]
        public IDataResult<List<OperationClaim>> GetAll()
        {
            var result = _operationClaimRepository.GetAll();
            return new SuccessDataResult<List<OperationClaim>>(result, Messages.GetAll);
        }

        public IDataResult<OperationClaim> GetById(string id)
        {
            var result = _operationClaimRepository.GetById(id);
            return new SuccessDataResult<OperationClaim>(result, Messages.GetById(id));
        }

        [SecuredOperation("admin")]
        public IResult Add(OperationClaim entity)
        {
            _operationClaimRepository.Insert(entity);
            return new SuccessResult(Messages.Add(entity.Name));
        }

        public OperationClaim ClaimForGetById(string id)
        {
            var result = _operationClaimRepository.GetById(id);
            return result;
        }

        [SecuredOperation("admin")]
        public IResult Update(OperationClaim entity)
        {
            if (entity.Id != null)
            {
                _operationClaimRepository.Update(entity);
                return new SuccessResult(Messages.Update(entity.Id));
            }

            return new ErrorResult(Messages.IdNotFound);

        }

        [SecuredOperation("admin")]
        public IResult Delete(OperationClaim entity)
        {
            if (entity.Id!=null)
            {
                _operationClaimRepository.Delete(entity);
                return new SuccessResult(Messages.Delete(entity.Id));
            }

            return new ErrorResult(Messages.IdNotFound);
        }
    }
}
