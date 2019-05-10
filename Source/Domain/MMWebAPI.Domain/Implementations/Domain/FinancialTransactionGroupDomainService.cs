using System.Collections.Generic;
using System.Threading.Tasks;
using MMWebAPI.Domain.Interfaces.Domain;
using MMWebAPI.Domain.Interfaces.Repository;
using MMWebAPI.Domain.Models;
using MMWebAPI.Domain.Validator;

namespace MMWebAPI.Domain.Implementations.Domain
{
    public class FinancialTransactionGroupDomainService : DomainServiceBase<FinancialTransactionGroup>, IFinancialTransactionGroupDomainService
    {
        private readonly IFinancialTransactionGroupRepository _repo;
        public FinancialTransactionGroupDomainService(IFinancialTransactionGroupRepository repo) : base(repo, new FinancialTransactionGroupValidator())
        {
            _repo = repo;
        }

        public async Task<List<FinancialTransactionGroup>> GetAllsimpleAsync()
        {
            return await _repo.GetAllSimpleAsync();
        }
    }
}
