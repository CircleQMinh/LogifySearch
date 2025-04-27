using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVTQ.LogifySearch.Domain.Model.Account
{
    public class UserSearchModel
    {
        public int Offset { get; set; }
        public int Limit { get; set; }
        public string Search { get; set; }
        public string Roles { get; set; }
    }
}
