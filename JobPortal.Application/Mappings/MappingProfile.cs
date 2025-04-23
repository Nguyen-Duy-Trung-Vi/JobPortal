using JobPortal.Application.DTOs;
using JobPortal.Domain.Entities;
using AutoMapper;

namespace JobPortal.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Company
            CreateMap<Company, CompanyDto>().ReverseMap();
            CreateMap<Company, CreateCompanyDto>().ReverseMap();

            // JobPost
            CreateMap<JobPost, JobPostDto>().ReverseMap();
            CreateMap<JobPost, CreateJobPostDto>().ReverseMap();

            // Candidate
            CreateMap<Candidate, CandidateDto>().ReverseMap();
            CreateMap<Candidate, CreateCandidateDto>().ReverseMap();

            // JobApplication
            CreateMap<JobApplication, JobApplicationDto>().ReverseMap();
            CreateMap<JobApplication, CreateJobApplicationDto>().ReverseMap();
        }
    }
}
