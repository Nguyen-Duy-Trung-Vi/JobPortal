using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Domain.Entities
{
    public class JobPost : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime PostedAt { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }
        public ICollection<JobApplication> Applications { get; set; } = new List<JobApplication>();
    }
}
