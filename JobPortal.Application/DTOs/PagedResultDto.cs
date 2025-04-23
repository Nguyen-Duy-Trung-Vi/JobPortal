using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Application.DTOs
{
    public class PagedResultDto<T> where T : class
    {
        public IEnumerable<T> Items { get; set; }
        public int TotalCount { get; set; }
    }
}
