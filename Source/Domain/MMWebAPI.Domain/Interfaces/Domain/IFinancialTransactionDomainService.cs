using System.Collections.Generic;
using System.Threading.Tasks;
using MMWebAPI.Domain.Models;
using MMWebAPI.Domain.Validator;

namespace MMWebAPI.Domain.Interfaces.Domain
{
    public interface IFinancialTransactionDomainService : IDomainServiceBase<FinancialTransaction>
    {
        Task<List<FinancialTransaction>> GetAllByGroup(int id);
    }
}
