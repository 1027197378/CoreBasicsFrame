using NetCore.Model.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace NetCore.Interfaces
{
    public interface ISYSUser : IBaseInterface<SYSUser>
    {
        Task<SYSUser> GetUser(string ID);
    }
}
