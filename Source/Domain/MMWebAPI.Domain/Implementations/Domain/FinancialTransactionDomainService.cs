using MMWebAPI.Domain.Exceptions;
using MMWebAPI.Domain.Interfaces.Domain;
using MMWebAPI.Domain.Interfaces.Repository;
using MMWebAPI.Domain.Models;
using MMWebAPI.Domain.Validator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMWebAPI.Domain.Implementations.Domain
{
    public class FinancialTransactionDomainService : DomainServiceBase<FinancialTransaction>, IFinancialTransactionDomainService
    {
        private readonly IFinancialTransactionRepository _repo;
        public FinancialTransactionDomainService(IFinancialTransactionRepository repo) : base(repo, new FinancialTransactionValidator())
        {
            _repo = repo;
        }

        public async Task<List<FinancialTransaction>> GetAllByGroup(int id)
        {
            var response = await _repo.GetAllByGroupAsync(id);
            if (!response.Any())
                throw new NoContentException("Nenhuma transação para este grupo");
            return response;
        }
    }
}
