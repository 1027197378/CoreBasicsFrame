using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NetCore.Interfaces
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
        /// <param name="Where">查询条件</param>
        /// <returns></returns>
        Task<List<TEntity>> QueryStrAsync(Expression<Func<TEntity, bool>> Where);

        /// <summary>
        /// Where条件查询排序
        /// </summary>
        /// <param name="Where">查询条件</param>
        /// <param name="OrderBy">排序字段</param>
        /// <param name="IsAsc">ASC or DESC</param>
        /// <returns></returns>
        Task<List<TEntity>> QueryStrAsync(Expression<Func<TEntity, bool>> Where, Expression<Func<TEntity, object>> OrderBy, bool IsAsc = true);

        /// <summary>
        /// Sql查询
        /// </summary>
        /// <param name="SqlStr">Sql语句</param>
        /// <returns></returns>
        Task<List<TEntity>> QueryForSql(string SqlStr);

        /// <summary>
        /// Sql参数查询
        /// </summary>
        /// <param name="SqlStr">Sql语句</param>
        /// <param name="Params">参数</param>
        /// <returns></returns>
        Task<List<TEntity>> QueryForSql(string SqlStr, params object[] Params);

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="Where"></param>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <param name="Total"></param>
        /// <returns></returns>
        List<TEntity> QueryPageList(Expression<Func<TEntity,bool>> Where,int PageIndex,int PageSize,out int Total );
    }
}
