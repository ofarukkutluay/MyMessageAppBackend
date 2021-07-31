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
        //invocation ile gelen isimlendirmede hata çıkardığı ve her business module eklenemediği için bağlılık çıkarıldı
        IDataResult<List<T>> GetAll();
        IResult Add(T entity);
        IDataResult<T> GetById(string id);
        IResult Update(T entity);
        IResult Delete(T entity);
    }
}
