using Microsoft.EntityFrameworkCore;
using MMWebAPI.Domain.Interfaces.Repository;
using MMWebAPI.Domain.Models;
using MMWebAPI.Infrastructure.Repository.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMWebAPI.Infrastructure.Repository.Implementation
{
    public class FinancialTransactionGroupRepository : RepositoryBase<FinancialTransactionGroup>, IFinancialTransactionGroupRepository
    {
        public FinancialTransactionGroupRepository(MMContext dbContext) : base(dbContext)
        {
        }
        public override async Task<List<FinancialTransactionGroup>> GetAllAsync()
        {
            var objList = await DbSet
                .Include(x=>x.FinancialTransactions)
                .ToListAsync();
            return objList;
        }

        public async Task<List<FinancialTransactionGroup>> GetAllSimpleAsync()
        {
            var result = await base.GetAllAsync();
            result.ForEach(g =>
           {
               g.TotalValue = DbContext.FinancialTransaction.Where(x=>x.GroupId == g.Id).Sum(x => x.Value);
           });
            return result;
        }

        public async override Task<FinancialTransactionGroup> GetByIdAsync(int entityId)
        {
            return await DbSet
                .Include(x => x.FinancialTransactions)
                .FirstOrDefaultAsync(x => x.Id == entityId)
                .ConfigureAwait(false);
        }
    }
}
