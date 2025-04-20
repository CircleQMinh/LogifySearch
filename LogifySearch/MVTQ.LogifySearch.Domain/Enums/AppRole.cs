using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVTQ.LogifySearch.Domain.Enums
{
    public enum AppRole
    {
        [Description("User")]
        User = 0,
        [Description("Admin")]
        Admin = 1,
        [Description("Manager")]
        Manager = 2,

    }
}
