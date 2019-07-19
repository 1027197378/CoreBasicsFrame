using BaseCore.InterFace;
using NetCore.Model;
using NetCore.Model.Models;
using NetCore.Services.BaseService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetCore.Services
{
    public class SYSUserService : BaseService<SYSUser>, ISYSUser
    {
        public SYSUserService(NetCoreBaseDBContext context) : base(context)
        {
        }

        public SYSUser QueryById(Guid id)
        {
            return base.QueryById(id);
        }

        public IEnumerable<SYSUser> LinqQuery(string Name)
        {
            var queryData = from data in _context.SYSUser
                            select data;
            return queryData ;
        }
    }
}
