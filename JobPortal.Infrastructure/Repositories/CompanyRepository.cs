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
    public class CompanyRepository : GenericRepository<Company>, ICompanyRepository
    {
        public CompanyRepository(JobPortalDbContext dbContext) : base(dbContext)
        {
        }

        // Bạn có thể thêm các phương thức riêng nếu cần cho Company
        public async Task<IEnumerable<Company>> GetByNameAsync(string name)
        {
            return await _dbSet.Where(c => c.Name.Contains(name)).ToListAsync();
        }
    }
}
