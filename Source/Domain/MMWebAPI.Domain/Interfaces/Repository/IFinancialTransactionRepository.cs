using MMWebAPI.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MMWebAPI.Domain.Interfaces.Repository
{
    public interface IFinancialTransactionRepository : IRepositoryBase<FinancialTransaction>
    {
        Task<List<FinancialTransaction>> GetAllByGroupAsync(int groupId);
    }
}
