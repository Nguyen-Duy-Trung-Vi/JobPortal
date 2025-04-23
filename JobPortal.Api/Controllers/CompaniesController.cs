using AutoMapper;
using JobPortal.Application.DTOs;
using JobPortal.Application.Interfaces.Services;
using JobPortal.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace JobPortal.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompaniesController : ControllerBase
    {
        private readonly ICompanyService _companyService;
        private readonly IJobPostService _jobPostService;

        public CompaniesController(ICompanyService companyService, IJobPostService jobPostService)
        {
            _companyService = companyService;
            _jobPostService = jobPostService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCompany([FromBody] CreateCompanyDto dto)
        {
            var result = await _companyService.AddAsync(dto);
            return CreatedAtAction(nameof(GetCompanyById), new { id = result.Id }, result);
        }

        [HttpPost("{id}/jobs")]
        public async Task<IActionResult> CreateJobPost(int id, [FromBody] CreateJobPostDto dto)
        {
            var result = await _jobPostService.AddAsync(id, dto);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCompanyById(int id)
        {
            var company = await _companyService.GetByIdAsync(id);
            return company is null ? NotFound() : Ok(company);
        }
    }
}
