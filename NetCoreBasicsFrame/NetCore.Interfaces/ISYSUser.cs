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

        IEnumerable<SYSUser> LinqQuery(string Name);

        Task<List<SYSUser>> QueryStrAsync(Expression<Func<SYSUser, bool>> where);

        /// <summary>
        /// ok
        /// </summary>
        /// <param name="where"></param>
        /// <param name="orderBy"></param>
        /// <param name="isASC"></param>
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

        List<SYSUser> pageList(Expression<Func<SYSUser, bool>> where, int pageindx, int pagesize, out int total);
    }
}
