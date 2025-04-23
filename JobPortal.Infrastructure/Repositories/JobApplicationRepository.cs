using JobPortal.Application.Interfaces.Repositories;
using JobPortal.Domain.Entities;
using JobPortal.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Infrastructure.Repositories
{
    public class JobApplicationRepository : GenericRepository<JobApplication>, IJobApplicationRepository
    {
        public JobApplicationRepository(JobPortalDbContext dbContext) : base(dbContext)
        {
        }
        public async Task<IEnumerable<JobApplication>> GetByJobPostIdAsync(int jobPostId)
        {
            return await _dbSet
                .Include(app => app.Candidate)
                .Where(app => app.JobPostId == jobPostId)
                .OrderByDescending(app => app.AppliedAt)
                .ToListAsync();
        }
    }
}
