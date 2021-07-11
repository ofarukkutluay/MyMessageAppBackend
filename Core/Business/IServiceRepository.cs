using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Results;

namespace Core.Business
{
    public interface IServiceRepository<T>
    {
        IDataResult<List<T>> GetAll();
        IResult Add(T entity);
        IDataResult<T> GetById(long id);
        IResult Update(T entity);
        IResult Delete(T entity);
    }
}
