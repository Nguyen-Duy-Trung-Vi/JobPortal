using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Application.DTOs
{
    public class CreateJobPostDto
    {
        [Required(ErrorMessage = "Title is required.")]
        [StringLength(255, ErrorMessage = "Title must not exceed 255 characters.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [StringLength(1000, ErrorMessage = "Description must not exceed 1000 characters.")]
        public string Description { get; set; }
    }
}
