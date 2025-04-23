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
    public class CandidateService : ICandidateService
    {
        private readonly ICandidateRepository _repo;
        private readonly IMapper _mapper;

        public CandidateService(ICandidateRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<CandidateDto> AddAsync(CreateCandidateDto dto)
        {
            try
            {
                var entity = _mapper.Map<Candidate>(dto);
                await _repo.AddAsync(entity);
                return _mapper.Map<CandidateDto>(entity);
            }
            catch (Exception ex)
            {
                throw new ServiceException("An error occurred while adding the candidate.", ex);
            }
        }
    }
}
