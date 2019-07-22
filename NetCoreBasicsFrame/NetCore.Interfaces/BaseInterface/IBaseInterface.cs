using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NetCore.Interfaces.BaseInterface
{
    public interface IBaseInterface<TEntity> where TEntity : class
    {
        /// <summary>
        /// 实体主键查询
        /// </summary>
        /// <param name="Id">主键</param>
        /// <returns></returns>
        Task<TEntity> QueryByIdAsync(object Id);

        /// <summary>
        /// Where条件查询
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <returns></returns>
        Task<List<TEntity>> QueryStrAsync(Expression<Func<TEntity, bool>> where);

        /// <summary>
        /// Where条件查询排序
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <param name="orderBy">排序字段</param>
        /// <param name="isAsc">ASC or DESC</param>
        /// <returns></returns>
        Task<List<TEntity>> QueryStrAsync(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, object>> orderBy, bool isAsc = true);

        /// <summary>
        /// Sql查询
        /// </summary>
        /// <param name="sqlStr">Sql语句</param>
        /// <returns></returns>
        Task<List<TEntity>> QueryForSql(string sqlStr);

        /// <summary>
        /// Sql参数查询
        /// </summary>
        /// <param name="sqlStr">Sql语句</param>
        /// <param name="param">参数</param>
        /// <returns></returns>
        Task<List<TEntity>> QueryForSql(string sqlStr, params object[] param);

    }
}
