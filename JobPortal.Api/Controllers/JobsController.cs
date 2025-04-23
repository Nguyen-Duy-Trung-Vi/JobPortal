using JobPortal.Application.DTOs;
using JobPortal.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace JobPortal.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JobsController : ControllerBase
    {
        private readonly IJobPostService _jobPostService;
        private readonly IJobApplicationService _applicationService;

        public JobsController(IJobPostService jobPostService, IJobApplicationService applicationService)
        {
            _jobPostService = jobPostService;
            _applicationService = applicationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetJobs([FromQuery] JobSearchFilterDto filter)
        {
            var result = await _jobPostService.SearchJobsAsync(filter);
            return Ok(result);
        }

        [HttpGet("{id}/applications")]
        public async Task<IActionResult> GetApplicationsForJob(int id)
        {
            var applications = await _applicationService.GetApplicationsByJobIdAsync(id);
            return Ok(applications);
        }
    }
}
