using MMWebAPI.Domain.Interfaces.Repository;
using MMWebAPI.Domain.Models;
using MMWebAPI.Infrastructure.Repository.Context;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace MMWebAPI.Infrastructure.Repository.Implementation
{
    public class FinancialTransactionRepository : RepositoryBase<FinancialTransaction>, IFinancialTransactionRepository
    {
        public FinancialTransactionRepository(MMContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<FinancialTransaction>> GetAllByGroupAsync(int groupId)
        {
            var response =  DbSet.Include(x=> x.Group)
                .ToListAsync().Result.Where(x => x.GroupId == groupId);

            return response.ToList();
        }
    }
}
