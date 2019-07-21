using Microsoft.EntityFrameworkCore;
using NetCore.Interfaces.BaseInterface;
using NetCore.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NetCore.Services.BaseService
{
    public class BaseService<Entity> : IBaseInterface<Entity> where Entity : class, new()
    {
        protected readonly NetCoreBaseDBContext _context;
        protected readonly DbSet<Entity> _dbSet;

        public BaseService(NetCoreBaseDBContext context)
        {
            this._context = context;
            _dbSet = context.Set<Entity>();
        }

        /// <summary>
        /// 根据实体主键获取数据
        /// </summary>
        /// <param name="Id">主键</param>
        /// <returns></returns>
        public Entity QueryById(object Id)
        {
            return  _dbSet.Find(Id);
        }

        /// <summary>
        /// 异步请求根据实体主键获取数据
        /// </summary>
        /// <param name="Id">主键</param>
        /// <returns></returns>
        public async Task<Entity> QueryByIdAsync(object Id)
        {
            return await _dbSet.FindAsync(Id);
        }
    }
}
