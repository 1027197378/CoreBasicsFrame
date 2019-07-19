using NetCore.Model.Models;
using System;
using System.Collections;
using System.Collections.Generic;

namespace BaseCore.InterFace
{
    public interface ISYSUser
    {
        SYSUser QueryById(Guid id);

        IEnumerable<SYSUser> LinqQuery(string Name);
    }
}
