using JobPortal.Application.DTOs;
using JobPortal.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace JobPortal.Api.Controllers
{
    [ApiController]
    [Route("api/applications")]
    public class JobApplicationController : ControllerBase
    {
        private readonly IJobApplicationService _service;

        public JobApplicationController(IJobApplicationService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Apply([FromBody] CreateJobApplicationDto dto)
        {
            var result = await _service.AddAsync(dto);
            return CreatedAtAction(nameof(GetByJob), new { id = dto.JobPostId }, result);
        }

        [HttpGet]
        public async Task<IActionResult> GetByJob(int id)
        {
            var list = await _service.GetApplicationsByJobIdAsync(id);
            return Ok(list);
        }
    }
}
