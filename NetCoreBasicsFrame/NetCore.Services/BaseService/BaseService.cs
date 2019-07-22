using Microsoft.EntityFrameworkCore;
using NetCore.Interfaces.BaseInterface;
using NetCore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NetCore.Services.BaseService
{
    public class BaseService<TEntity> : IBaseInterface<TEntity> where TEntity : class, new()
    {
        protected readonly NetCoreBaseDBContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        public BaseService(NetCoreBaseDBContext context)
        {
            this._context = context;
            _dbSet = context.Set<TEntity>();
        }

        /// <summary>
        /// 实体主键查询
        /// </summary>
        /// <param name="Id">主键</param>
        /// <returns></returns>
        public async Task<TEntity> QueryByIdAsync(object Id)
        {
            return await _dbSet.FindAsync(Id);
        }

        /// <summary>
        /// Where条件查询
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public async Task<List<TEntity>> QueryStrAsync(Expression<Func<TEntity, bool>> where)
        {
            return where == null ? await _dbSet.ToListAsync() : await _dbSet.Where(where).ToListAsync(); ;
        }

        /// <summary>
        /// Where条件查询排序
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <param name="orderBy">排序字段不能为Null</param>
        /// <param name="isAsc">ASC or DESC</param>
        /// <returns></returns>
        public async Task<List<TEntity>> QueryStrAsync(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, object>> orderBy, bool isAsc = true)
        {
            if (isAsc)
            {
                return where == null ? await _dbSet.OrderBy(orderBy).ToListAsync() : await _dbSet.Where(where).OrderBy(orderBy).ToListAsync();
            }
            return where == null ? await _dbSet.OrderByDescending(orderBy).ToListAsync() : await _dbSet.Where(where).OrderByDescending(orderBy).ToListAsync();
        }

        /// <summary>
        /// Sql查询
        /// </summary>
        /// <param name="sqlStr">Sql语句</param>
        /// <returns></returns>
        public async Task<List<TEntity>> QueryForSql(string sqlStr)
        {
            return await _dbSet.FromSql(sqlStr).ToListAsync();
        }

        /// <summary>
        /// Sql查询
        /// </summary>
        /// <param name="sqlStr">Sql语句</param>
        /// <param name="param">参数</param>
        /// <returns></returns>
       public async Task<List<TEntity>> QueryForSql(string sqlStr, params object[] param)
        {
            return await _dbSet.FromSql(sqlStr,param).ToListAsync();
        }

    }
}
