using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities.Concretes;
using Core.Utilities.Results;

namespace Business.Abstracts
{
    public interface IOperationClaimService
    {
        IDataResult<List<OperationClaim>> GetAll();
        IDataResult<OperationClaim> GetById(string id);
        IResult Add(OperationClaim entity);
        OperationClaim ClaimForGetById(string id);
        IResult Update(OperationClaim entity);
        IResult Delete(OperationClaim entity);
    }
}
