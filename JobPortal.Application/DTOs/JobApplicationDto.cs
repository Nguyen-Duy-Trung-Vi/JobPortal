using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Application.DTOs
{
    public class JobApplicationDto
    {
        public int Id { get; set; }
        public int CandidateId { get; set; }
        public int JobPostId { get; set; }
        public string ResumeUrl { get; set; }
        public DateTime AppliedAt { get; set; }
    }
}
