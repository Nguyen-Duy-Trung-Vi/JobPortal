using AutoMapper;
using JobPortal.Application.Common.Exceptions;
using JobPortal.Application.DTOs;
using JobPortal.Application.Interfaces.Repositories;
using JobPortal.Application.Mappings;
using JobPortal.Application.Services;
using JobPortal.Domain.Entities;
using Moq;

namespace JobPortal.Tests
{
    public class JobApplicationServiceTests
    {
        private readonly Mock<IJobApplicationRepository> _mockJobAppRepo;
        private readonly Mock<IJobPostRepository> _mockJobPostRepo;
        private readonly Mock<ICandidateRepository> _mockCandidateRepo;
        private readonly IMapper _mapper;
        private readonly JobApplicationService _service;

        public JobApplicationServiceTests()
        {
            _mockJobAppRepo = new Mock<IJobApplicationRepository>();
            _mockJobPostRepo = new Mock<IJobPostRepository>();
            _mockCandidateRepo = new Mock<ICandidateRepository>();

            var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            _mapper = config.CreateMapper();

            _service = new JobApplicationService(_mockJobAppRepo.Object, _mockJobPostRepo.Object, _mockCandidateRepo.Object, _mapper);
        }

        #region AddAsync Unit Test
        [Fact]
        public async Task AddAsync_ShouldSetAppliedAt()
        {
            // Arrange
            var dto = new CreateJobApplicationDto
            {
                CandidateId = 1,
                JobPostId = 2,
                ResumeUrl = "cv.pdf"
            };

            var jobPost = new JobPost { Id = 2, Title = "Job Title", CompanyId = 1 };
            var candidate = new Candidate { Id = 1, FullName = "John Doe", Email = "john@example.com" };

            _mockJobPostRepo.Setup(r => r.GetByIdAsync(dto.JobPostId)).ReturnsAsync(jobPost);
            _mockCandidateRepo.Setup(r => r.GetByIdAsync(dto.CandidateId)).ReturnsAsync(candidate);
            _mockJobAppRepo.Setup(r => r.AddAsync(It.IsAny<JobApplication>())).Returns(Task.CompletedTask);

            // Act
            var result = await _service.AddAsync(dto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(dto.JobPostId, result.JobPostId);
            Assert.Equal(dto.CandidateId, result.CandidateId);
            Assert.NotEqual(default(DateTime), result.AppliedAt);  // Ensure AppliedAt is set
            _mockJobAppRepo.Verify(r => r.AddAsync(It.Is<JobApplication>(a =>
                a.AppliedAt != default && a.ResumeUrl == dto.ResumeUrl)), Times.Once);
        }
        #endregion

        #region AddAsync Exception Unit Test
        [Fact]
        public async Task AddAsync_ShouldThrowServiceException_WhenErrorOccursInRepository()
        {
            // Arrange
            var dto = new CreateJobApplicationDto { CandidateId = 1, JobPostId = 2, ResumeUrl = "https://localhost:7228/swagger/cv.pdf" };
            _mockJobPostRepo.Setup(r => r.GetByIdAsync(dto.JobPostId)).ReturnsAsync(new JobPost { Id = 2 });
            _mockCandidateRepo.Setup(r => r.GetByIdAsync(dto.CandidateId)).ReturnsAsync(new Candidate { Id = 1 });
            _mockJobAppRepo.Setup(r => r.AddAsync(It.IsAny<JobApplication>())).ThrowsAsync(new Exception("Database error"));

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ServiceException>(() => _service.AddAsync(dto));
            Assert.Equal("An error occurred while processing the job application.", exception.Message);
        }
        #endregion

        #region GetApplicationsByJobIdAsync Unit Test
        [Fact]
        public async Task GetApplicationsByJobIdAsync_ShouldReturnApplications()
        {
            // Arrange
            var jobPostId = 2;
            var jobPost = new JobPost { Id = jobPostId, Title = "Job Title", CompanyId = 1 };

            var applications = new List<JobApplication>
        {
            new JobApplication { CandidateId = 1, JobPostId = jobPostId, AppliedAt = DateTime.UtcNow },
            new JobApplication { CandidateId = 2, JobPostId = jobPostId, AppliedAt = DateTime.UtcNow.AddDays(-1) }
        };

            _mockJobPostRepo.Setup(r => r.GetByIdAsync(jobPostId)).ReturnsAsync(jobPost);
            _mockJobAppRepo.Setup(r => r.GetByJobPostIdAsync(jobPostId)).ReturnsAsync(applications);

            // Act
            var result = await _service.GetApplicationsByJobIdAsync(jobPostId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());  // Ensure two applications are returned
            Assert.All(result, app => Assert.Equal(jobPostId, app.JobPostId));  // Check if jobPostId matches
        }
        #endregion
    }
}
