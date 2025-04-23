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
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;

        public CompanyService(ICompanyRepository companyRepository, IMapper mapper)
        {
            _companyRepository = companyRepository;
            _mapper = mapper;
        }

        public async Task<CompanyDto> AddAsync(CreateCompanyDto dto)
        {
            try
            {
                var entity = _mapper.Map<Company>(dto);
                await _companyRepository.AddAsync(entity);
                return _mapper.Map<CompanyDto>(entity);
            }
            catch (Exception ex)
            {
                throw new ServiceException("An error occurred while adding the company.", ex);
            }
        }

        public async Task<CompanyDto?> GetByIdAsync(int id)
        {
            try
            {
                var company = await _companyRepository.GetByIdAsync(id);
                return company == null ? null : _mapper.Map<CompanyDto>(company);
            }
            catch (Exception ex)
            {
                throw new ServiceException("An error occurred while fetching the company.", ex);
            }
        }
    }
}
