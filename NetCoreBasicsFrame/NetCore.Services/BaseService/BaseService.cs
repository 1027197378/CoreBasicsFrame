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
        /// <param name="Where"></param>
        /// <returns></returns>
        public async Task<List<TEntity>> QueryStrAsync(Expression<Func<TEntity, bool>> Where)
        {
            return await _dbSet.Where(Where).ToListAsync(); ;
        }

        /// <summary>
        /// Where条件查询排序
        /// </summary>
        /// <param name="Where">查询条件</param>
        /// <param name="OrderBy">排序字段不能为Null</param>
        /// <param name="IsAsc">ASC or DESC</param>
        /// <returns></returns>
        public async Task<List<TEntity>> QueryStrAsync(Expression<Func<TEntity, bool>> Where, Expression<Func<TEntity, object>> OrderBy, bool IsAsc = true)
        {
            if (IsAsc)
            {
                return await _dbSet.Where(Where).OrderBy(OrderBy).ToListAsync();
            }
            return await _dbSet.Where(Where).OrderByDescending(OrderBy).ToListAsync();
        }

        /// <summary>
        /// Sql查询
        /// </summary>
        /// <param name="SqlStr">Sql语句</param>
        /// <returns></returns>
        public async Task<List<TEntity>> QueryForSql(string SqlStr)
        {
            return await _dbSet.FromSql(SqlStr).ToListAsync();
        }

        /// <summary>
        /// Sql查询
        /// </summary>
        /// <param name="SqlStr">Sql语句</param>
        /// <param name="Params">参数</param>
        /// <returns></returns>
        public async Task<List<TEntity>> QueryForSql(string SqlStr, params object[] Params)
        {
            return await _dbSet.FromSql(SqlStr, Params).ToListAsync();
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="Where"></param>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <param name="Total"></param>
        /// <returns></returns>
        public List<TEntity> QueryPageList(Expression<Func<TEntity, bool>> Where, int PageIndex, int PageSize, out int Total)
        {
            List<TEntity> entity = new List<TEntity>();
            entity = _dbSet.Where(Where).ToList();
            Total = entity.Count;
            return entity.Skip(PageSize * (PageIndex - 1)).Take(PageSize).ToList();
        }
    }
}
