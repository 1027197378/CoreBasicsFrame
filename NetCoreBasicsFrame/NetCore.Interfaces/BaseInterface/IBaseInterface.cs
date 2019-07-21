using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NetCore.Interfaces.BaseInterface
{
    public interface IBaseInterface<TEntity> where TEntity : class
    {
       TEntity QueryById(object Id);
       Task<TEntity> QueryByIdAsync(object Id);
    }
}
