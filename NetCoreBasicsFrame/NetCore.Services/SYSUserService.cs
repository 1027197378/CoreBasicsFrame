using NetCore.Interfaces;
using NetCore.Model;
using NetCore.Model.Models;
using NetCore.Services.BaseService;
using System;
using System.Collections;
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

        public async Task<SYSUser> GetUser(string ID)
        {
            return await QueryByIdAsync(ID);
        }
    }
}

