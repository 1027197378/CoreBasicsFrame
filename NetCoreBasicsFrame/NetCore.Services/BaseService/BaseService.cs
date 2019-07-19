using BaseCore.InterFace.IBase;
using NetCore.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.Services.BaseService
{
    public class BaseService<Entity> : IBaseInterFace<Entity> where Entity : class, new()
    {
        protected readonly NetCoreBaseDBContext _context;

        public BaseService(NetCoreBaseDBContext context)
        {
            this._context = context;
        }
        public Entity QueryById(object Id)
        {
           return _context.Set<Entity>().Find(Id);
        }
    }
}
