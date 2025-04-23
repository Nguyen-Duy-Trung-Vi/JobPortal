using JobPortal.Application.Common;
using JobPortal.Application.DTOs;
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
    public class JobPostRepository : GenericRepository<JobPost>, IJobPostRepository
    {
        public JobPostRepository(JobPortalDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<PagedResult<JobPost>> SearchAsync(JobSearchFilterDto filter)
        {
            var query = _dbSet
                .Include(j => j.Company)
                .AsQueryable();

            if (!string.IsNullOrEmpty(filter.Keyword))
            {
                filter.Keyword = filter.Keyword.ToUpper();
                query = query.Where(j =>
                    j.Title.ToUpper().Contains(filter.Keyword) ||
                    j.Description.ToUpper().Contains(filter.Keyword));
            }

            if (filter.CompanyId.HasValue)
            {
                query = query.Where(j => j.CompanyId == filter.CompanyId.Value);
            }

            if (filter.FromDate.HasValue)
            {
                query = query.Where(j => j.PostedAt >= filter.FromDate.Value);
            }

            if (filter.ToDate.HasValue)
            {
                query = query.Where(j => j.PostedAt <= filter.ToDate.Value);
            }

            var totalCount = await query.CountAsync();

            var items = await query
                .OrderByDescending(j => j.PostedAt)
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToListAsync();

            return new PagedResult<JobPost>(items, totalCount);
        }
    }
}
