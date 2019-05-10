using MMWebAPI.Application.InOut.FinancialTransaction;
using MMWebAPI.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MMWebAPI.Application.Interfaces
{
    public interface IFinancialTransactionApplicationService : IApplicationServiceBase<FinancialTransactionRequest, FinancialTransactionResponse>
    {
        Task<List<FinancialTransactionResponse>> GetAllByGroup(int id);
    }
}
