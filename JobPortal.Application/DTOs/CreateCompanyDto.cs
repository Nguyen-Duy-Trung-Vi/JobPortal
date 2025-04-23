using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Application.DTOs
{
    public class CreateCompanyDto
    {
        [Required(ErrorMessage = "Company name is required.")]
        [StringLength(255, ErrorMessage = "Company name must not exceed 255 characters.")]
        public string Name { get; set; }
    }
}
