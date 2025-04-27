using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVTQ.LogifySearch.Domain.Common
{
    public class PaginationFilter
    {
        public int Offset { get; set; }
        public int Limit { get; set; }
        public PaginationFilter()
        {
            this.Offset = 1;
            this.Limit = 10;
        }
        public PaginationFilter(int offset, int limit)
        {
            this.Offset = offset;
            this.Limit = limit;
        }
    }
}
