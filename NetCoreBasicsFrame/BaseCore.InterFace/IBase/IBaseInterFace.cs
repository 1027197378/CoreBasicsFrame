using System;
using System.Collections.Generic;
using System.Text;

namespace BaseCore.InterFace.IBase
{
    public interface IBaseInterFace<TEntity> where TEntity : class
    {
        TEntity QueryById(object Id);
    }
}
