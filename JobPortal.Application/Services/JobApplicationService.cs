using AutoMapper;
using JobPortal.Application.Common.Exceptions;
using JobPortal.Application.DTOs;
using JobPortal.Application.Interfaces.Repositories;
using JobPortal.Application.Interfaces.Services;
using JobPortal.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Application.Services
{
    public class JobApplicationService : IJobApplicationService
    {
        private readonly IJobApplicationRepository _jobApplicationRepo;
        private readonly IJobPostRepository _jobPostRepo;
        private readonly ICandidateRepository _candidateRepo;
        private readonly IMapper _mapper;

        public JobApplicationService(
            IJobApplicationRepository jobApplicatioinRepo,
            IJobPostRepository jobPostRepo,
            ICandidateRepository candidateRepo,
            IMapper mapper)
        {
            _jobApplicationRepo = jobApplicatioinRepo;
            _jobPostRepo = jobPostRepo;
            _candidateRepo = candidateRepo;
            _mapper = mapper;
        }

        public async Task<JobApplicationDto> AddAsync(CreateJobApplicationDto dto)
        {
            try
            {
                var candidate = await _candidateRepo.GetByIdAsync(dto.CandidateId);
                if (candidate == null)
                    throw new NotFoundException("Candidate not found.");

                var job = await _jobPostRepo.GetByIdAsync(dto.JobPostId);
                if (job == null)
                    throw new NotFoundException("Job post not found.");

                var application = _mapper.Map<JobApplication>(dto);
                application.AppliedAt = DateTime.UtcNow;

                await _jobApplicationRepo.AddAsync(application);
                return _mapper.Map<JobApplicationDto>(application);
            }
            catch (Exception ex)
            {
                throw new ServiceException("An error occurred while processing the job application.", ex);
            }
        }

        public async Task<IEnumerable<JobApplicationDto>> GetApplicationsByJobIdAsync(int jobPostId)
        {
            try
            {
                var job = await _jobPostRepo.GetByIdAsync(jobPostId);
                if (job == null)
                    throw new NotFoundException("Job post not found.");

                var list = await _jobApplicationRepo.GetByJobPostIdAsync(jobPostId);
                return _mapper.Map<IEnumerable<JobApplicationDto>>(list);
            }
            catch (Exception ex)
            {
                throw new ServiceException("An error occurred while fetching job applications.", ex);
            }
        }
    }
}
