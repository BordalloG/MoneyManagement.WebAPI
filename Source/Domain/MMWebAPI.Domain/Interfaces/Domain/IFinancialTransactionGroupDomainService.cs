using System.Collections.Generic;
using System.Threading.Tasks;
using MMWebAPI.Domain.Models;

namespace MMWebAPI.Domain.Interfaces.Domain
{
    public interface IFinancialTransactionGroupDomainService : IDomainServiceBase<FinancialTransactionGroup>
    {
        Task<List<FinancialTransactionGroup>> GetAllsimpleAsync();
    }
}
