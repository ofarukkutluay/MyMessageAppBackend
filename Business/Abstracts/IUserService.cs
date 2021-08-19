using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Business;
using Core.Entities.Concretes;
using Core.Utilities.Results;

namespace Business.Abstracts
{
    public interface IUserService
    {
        IDataResult<List<User>> GetAll();
        IResult Add(User entity);
        IDataResult<User> GetById(string id);
        IDataResult<Person> GetPersonByUserId(string userId);
        IDataResult<Person> GetPersonByEmail(string email);
        IDataResult<Person> GetPersonByMobileNumber(string mobileNumber);
        IResult Update(User entity);
        IResult UpdatePerson(Person entity);
        IResult ActivateUser(string id);
        IResult Delete(User entity);
        List<OperationClaim> GetClaims(User user);
        User GetByMail(string email);
        User GetByMobileNumber(string mobileNumber);
    }
}
