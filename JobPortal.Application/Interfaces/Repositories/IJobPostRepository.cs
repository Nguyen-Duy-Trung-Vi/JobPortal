using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobPortal.Application.Common;
using JobPortal.Application.DTOs;
using JobPortal.Domain.Entities;

namespace JobPortal.Application.Interfaces.Repositories
{
    public interface IJobPostRepository : IGenericRepository<JobPost>
    {
        Task<PagedResult<JobPost>> SearchAsync(JobSearchFilterDto filter);
    }
}
