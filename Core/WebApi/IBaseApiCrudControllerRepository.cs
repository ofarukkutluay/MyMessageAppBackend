using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Business;
using Core.Entities.Concretes;
using Microsoft.AspNetCore.Mvc;

namespace Core.WebApi
{
    public interface IBaseApiCrudControllerRepository<TEntity>
    {
        public IActionResult Add(TEntity entity);
        public IActionResult GetAll();
        public IActionResult Delete(TEntity entity);
        public IActionResult Update(TEntity entity);
    }
}
