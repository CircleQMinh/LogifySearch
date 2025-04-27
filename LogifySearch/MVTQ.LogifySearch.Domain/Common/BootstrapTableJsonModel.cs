using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVTQ.LogifySearch.Domain.Common
{
    public class BootstrapTableJsonModel<T> where T : class
    {
        public List<T> Items { get; set; }
        public int Count { get; set; }
        public BootstrapTableJsonModel()
        {
            Items = new List<T>();
        }
    }
}
