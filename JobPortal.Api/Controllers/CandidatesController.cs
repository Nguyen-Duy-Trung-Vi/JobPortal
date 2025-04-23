using JobPortal.Application.DTOs;
using JobPortal.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace JobPortal.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CandidatesController : ControllerBase
    {
        private readonly ICandidateService _candidateService;

        public CandidatesController(ICandidateService candidateService)
        {
            _candidateService = candidateService;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterCandidate([FromBody] CreateCandidateDto dto)
        {
            var result = await _candidateService.AddAsync(dto);
            return Ok(result);
        }
    }
}
