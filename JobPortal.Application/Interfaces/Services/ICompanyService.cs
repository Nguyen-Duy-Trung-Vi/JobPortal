using JobPortal.Application.DTOs;
using JobPortal.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Application.Interfaces.Services
{
    public interface ICompanyService
    {
        Task<CompanyDto> AddAsync(CreateCompanyDto dto);
        Task<CompanyDto?> GetByIdAsync(int id);
    }
}
