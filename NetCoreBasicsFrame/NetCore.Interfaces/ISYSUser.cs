using NetCore.Model.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetCore.Interfaces
{
    public interface ISYSUser
    {
        Task<SYSUser> QueryById(object id);

        IEnumerable<SYSUser> LinqQuery(string Name);
    }
}
