using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Application.DTOs
{
    public class CreateJobApplicationDto
    {
        [Required(ErrorMessage = "CandidateId is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "CandidateId must be greater than 0.")]
        public int CandidateId { get; set; }

        [Required(ErrorMessage = "JobPostId is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "JobPostId must be greater than 0.")]
        public int JobPostId { get; set; }

        [Required(ErrorMessage = "Resume URL is required.")]
        [Url(ErrorMessage = "Resume URL must be a valid URL.")]
        public string ResumeUrl { get; set; }
    }
}
