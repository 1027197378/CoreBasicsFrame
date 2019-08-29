using NetCore.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace NetCore.Interfaces
{
    public interface ISYSUser
    {

        Task<SYSUser> QueryById(Guid id);

        /// <summary>
        /// 异步查询
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <param name="orderBy">排序</param>
        /// <param name="isASC">排序方式</param>
        /// <returns></returns>
        Task<List<SYSUser>> QueryStrAsync(Expression<Func<SYSUser, bool>> where);

        /// <summary>
        /// 异步查询
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <param name="orderBy">排序</param>
        /// <param name="isASC">排序方式</param>
        /// <returns></returns>
        Task<List<SYSUser>> QueryStrAsync(Expression<Func<SYSUser, bool>> where, Expression<Func<SYSUser, object>> orderBy, bool isASC = true);

        /// <summary>
        /// Sql查询
        /// </summary>
        /// <param name="sqlStr">Sql语句</param>
        /// <returns></returns>
        Task<List<SYSUser>> QueryForSql(string sqlStr);

        /// <summary>
        /// Sql参数查询
        /// </summary>
        /// <param name="sqlStr">Sql语句</param>
        /// <param name="param">参数</param>
        /// <returns></returns>
        Task<List<SYSUser>> QueryForSql(string sqlStr, params object[] param);

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="where">条件</param>
        /// <param name="pageindx">页码</param>
        /// <param name="pagesize">页数</param>
        /// <param name="total">总数</param>
        /// <returns></returns>
        List<SYSUser> PageList(Expression<Func<SYSUser, bool>> where, int pageindx, int pagesize, out int total);

        string getAop();
    }
}
