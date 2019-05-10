using AutoMapper;
using MMWebAPI.Application.InOut.FinancialTransactionGroup;
using MMWebAPI.Application.Interfaces;
using MMWebAPI.Domain.Interfaces.Domain;
using MMWebAPI.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MMWebAPI.Application.Implementations
{
    public class FinancialTransactionGroupApplicationService : ApplicationServiceBase<FinancialTransactionGroup, FinancialTransactionGroupRequest, FinancialTransactionGroupResponse>, IFinancialTransactionGroupApplicationService
    {
        private readonly IMapper _mapper;
        IFinancialTransactionGroupDomainService _domain;

        public FinancialTransactionGroupApplicationService(IFinancialTransactionGroupDomainService domain, IMapper mapper) : base(domain, mapper)
        {
            _domain = domain;
            _mapper = mapper;
        }

        public async Task<List<FinancialTransactionGroupResponse>> GetAllSimpleAsync()
        {
            var result = await _domain.GetAllsimpleAsync();
            var resultResponse = _mapper.Map<List<FinancialTransactionGroupResponse>>(result);
            return resultResponse;
        }
    }
}
