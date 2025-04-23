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
    public class JobPostService : IJobPostService
    {
        private readonly IJobPostRepository _jobPostRepo;
        private readonly ICompanyRepository _companyRepo;
        private readonly IMapper _mapper;

        public JobPostService(IJobPostRepository jobPostRepo, ICompanyRepository companyRepo, IMapper mapper)
        {
            _jobPostRepo = jobPostRepo;
            _companyRepo = companyRepo;
            _mapper = mapper;
        }

        public async Task<JobPostDto> AddAsync(int companyId, CreateJobPostDto dto)
        {
            try
            {
                var company = await _companyRepo.GetByIdAsync(companyId);
                if (company == null)
                    throw new NotFoundException("Company not found");

                var job = _mapper.Map<JobPost>(dto);
                job.CompanyId = companyId;
                job.PostedAt = DateTime.UtcNow;

                await _jobPostRepo.AddAsync(job);
                return _mapper.Map<JobPostDto>(job);
            }
            catch (Exception ex)
            {
                throw new ServiceException("An error occurred while adding the job.", ex);
            }
        }

        public async Task<PagedResultDto<JobPostDto>> SearchJobsAsync(JobSearchFilterDto filter)
        {
            try
            {
                var result = await _jobPostRepo.SearchAsync(filter);
                return new PagedResultDto<JobPostDto>
                {
                    Items = _mapper.Map<IEnumerable<JobPostDto>>(result.Items),
                    TotalCount = result.TotalCount
                };
            }
            catch (Exception ex)
            {
                throw new ServiceException("An error occurred while fetching the jobs.", ex);
            }
        }
    }
}
