using NetCore.Interfaces;
using NetCore.Interfaces.BaseInterface;
using NetCore.Model;
using NetCore.Model.Models;
using NetCore.Services.BaseService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NetCore.Services
{
    public class SYSUserService : BaseService<SYSUser>, ISYSUser
    {

        public SYSUserService(NetCoreBaseDBContext _context) : base(_context)
        {

        }

        public async Task<SYSUser> QueryById(Guid id)
        {
            return await base.QueryByIdAsync(id);
        }

        public IEnumerable<SYSUser> LinqQuery(string Name)
        {
            var queryData = from data in _context.SYSUser
                            select data;
            return queryData;
        }

        public async Task<List<SYSUser>> QueryStrAsync(Expression<Func<SYSUser, bool>> where)
        {
            return await base.QueryStrAsync(where);
        }

        public async Task<List<SYSUser>> QueryStrAsync(Expression<Func<SYSUser, bool>> where, Expression<Func<SYSUser, object>> orderBy, bool isASC = true)
        {
            return await base.QueryStrAsync(where, orderBy);
        }

        public async Task<List<SYSUser>> QueryForSql(string sqlStr)
        {
            return await base.QueryForSql(sqlStr);
        }

        public async Task<List<SYSUser>> QueryForSql(string sqlStr, params object[] param)
        {
            return await base.QueryForSql(sqlStr, param);
        }
    }
}

