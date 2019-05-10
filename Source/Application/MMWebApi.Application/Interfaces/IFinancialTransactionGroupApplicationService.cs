using System.Collections.Generic;
using System.Threading.Tasks;
using MMWebAPI.Application.InOut.FinancialTransactionGroup;
using MMWebAPI.Domain.Models;

namespace MMWebAPI.Application.Interfaces
{
    public interface IFinancialTransactionGroupApplicationService : IApplicationServiceBase<FinancialTransactionGroupRequest, FinancialTransactionGroupResponse>
    {
        Task<List<FinancialTransactionGroupResponse>> GetAllSimpleAsync();
    }
}
