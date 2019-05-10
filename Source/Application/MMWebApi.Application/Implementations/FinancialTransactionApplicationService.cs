using AutoMapper;
using MMWebAPI.Application.InOut.FinancialTransaction;
using MMWebAPI.Application.Interfaces;
using MMWebAPI.Domain.Interfaces.Domain;
using MMWebAPI.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MMWebAPI.Application.Implementations
{
    public class FinancialTransactionApplicationService : ApplicationServiceBase<FinancialTransaction, FinancialTransactionRequest, FinancialTransactionResponse>, IFinancialTransactionApplicationService
    {
        private readonly IFinancialTransactionDomainService  _domain;
        private readonly IMapper _mapper;
        public FinancialTransactionApplicationService(IFinancialTransactionDomainService domain, IMapper mapper) : base(domain, mapper)
        {
            _mapper = mapper;
            _domain = domain;
        }

        public async Task<List<FinancialTransactionResponse>> GetAllByGroup(int id)
        {
            return _mapper.Map<List<FinancialTransactionResponse>>( await _domain.GetAllByGroup(id));
        }
    }
}
