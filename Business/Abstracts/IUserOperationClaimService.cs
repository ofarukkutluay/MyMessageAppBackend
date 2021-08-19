using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities.Concretes;
using Core.Utilities.Results;

namespace Business.Abstracts
{
    public interface IUserOperationClaimService
    {
        IDataResult<List<UserOperationClaim>> GetAll();
        IResult Add(UserOperationClaim entity);
        IDataResult<UserOperationClaim> GetById(string id);
        IResult Update(UserOperationClaim entity);
        IResult Delete(UserOperationClaim entity);
        IDataResult<List<UserOperationClaim>> GetByUserId(string userId);
        List<UserOperationClaim> ClaimForGetByUserId(string userId);
    }
}
