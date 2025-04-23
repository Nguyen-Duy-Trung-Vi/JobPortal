using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Domain.Entities
{
    public class JobApplication : BaseEntity
    {
        public int CandidateId { get; set; }
        public Candidate Candidate { get; set; }
        public int JobPostId { get; set; }
        public JobPost JobPost { get; set; }
        public string ResumeUrl { get; set; }
        public DateTime AppliedAt { get; set; }
    }
}
