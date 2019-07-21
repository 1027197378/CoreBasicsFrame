using NetCore.Interfaces;
using NetCore.Interfaces.BaseInterface;
using NetCore.Model;
using NetCore.Model.Models;
using NetCore.Services.BaseService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCore.Services
{
    public class SYSUserService : BaseService<SYSUser>, ISYSUser
    {
     
        public SYSUserService(NetCoreBaseDBContext _context):base(_context)
        {

        }

        public async Task<SYSUser> QueryById(Guid id)
        {
            return await base.QueryById(id);
        }

        public IEnumerable<SYSUser> LinqQuery(string Name)
        {
            var queryData = from data in _context.SYSUser
                            select data;
            return queryData;
        }

    }
}
