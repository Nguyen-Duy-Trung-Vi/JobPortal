using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Domain.Entities
{
    public class Company : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<JobPost> JobPosts { get; set; } = new List<JobPost>();
    }
}
