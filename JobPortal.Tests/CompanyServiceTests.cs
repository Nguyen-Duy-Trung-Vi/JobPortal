using Xunit;
using Moq;
using AutoMapper;
using JobPortal.Application.Services;
using JobPortal.Application.DTOs;
using JobPortal.Application.Interfaces.Repositories;
using JobPortal.Domain.Entities;
using JobPortal.Application.Mappings;
using JobPortal.Application.Common.Exceptions;

namespace JobPortal.Tests
{
    public class CompanyServiceTests
    {
        private readonly Mock<ICompanyRepository> _mockRepo;
        private readonly IMapper _mapper;
        private readonly CompanyService _service;

        public CompanyServiceTests()
        {
            _mockRepo = new Mock<ICompanyRepository>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = config.CreateMapper();

            _service = new CompanyService(_mockRepo.Object, _mapper);
        }

        [Fact]
        public async Task AddAsync_ShouldReturnCompanyDto_WhenValid()
        {
            // Arrange
            var dto = new CreateCompanyDto { Name = "New Company" };
            Company? capturedEntity = null;

            _mockRepo.Setup(r => r.AddAsync(It.IsAny<Company>()))
                .Callback<Company>(c => capturedEntity = c)
                .Returns(Task.CompletedTask);

            // Act
            var result = await _service.AddAsync(dto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(dto.Name, result.Name);
            Assert.NotNull(capturedEntity);
            Assert.Equal(dto.Name, capturedEntity?.Name);
        }

        [Fact]
        public async Task AddAsync_ShouldThrowServiceException_WhenRepoFails()
        {
            // Arrange
            var dto = new CreateCompanyDto { Name = "Exception Co" };
            _mockRepo.Setup(r => r.AddAsync(It.IsAny<Company>()))
                .ThrowsAsync(new Exception("DB error"));

            // Act & Assert
            var ex = await Assert.ThrowsAsync<ServiceException>(() => _service.AddAsync(dto));
            Assert.Equal("An error occurred while adding the company.", ex.Message);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnNull_WhenCompanyNotFound()
        {
            // Arrange
            _mockRepo.Setup(r => r.GetByIdAsync(2)).ReturnsAsync((Company?)null);

            // Act
            var result = await _service.GetByIdAsync(2);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldThrowServiceException_WhenRepoFails()
        {
            // Arrange
            _mockRepo.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
                     .ThrowsAsync(new Exception("DB failure"));

            // Act & Assert
            var ex = await Assert.ThrowsAsync<ServiceException>(() => _service.GetByIdAsync(1));
            Assert.Contains("fetching the company.", ex.Message);
        }
    }
}