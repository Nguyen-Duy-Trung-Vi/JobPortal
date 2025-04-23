using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Application.Common
{
    public class PagedResult<T> where T : class
    {
        public PagedResult(IEnumerable<T> items, int totalCount)
        {
            Items = items;
            TotalCount = totalCount;
        }
        public IEnumerable<T> Items { get; set; }
        public int TotalCount { get; set; }
    }
}
