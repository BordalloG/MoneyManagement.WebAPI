using Microsoft.EntityFrameworkCore;
using MMWebAPI.Domain.Interfaces.Repository;
using MMWebAPI.Domain.Models;
using MMWebAPI.Infrastructure.Repository.Context;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MMWebAPI.Infrastructure.Repository.Implementation
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity>
        where TEntity : Entity
    {
        protected DbSet<TEntity> DbSet;
        protected MMContext DbContext;

        public RepositoryBase(MMContext dbContext)
        {
            DbContext = dbContext;
            DbSet = dbContext.Set<TEntity>();
        }

        public virtual async Task AddAsync(TEntity entity)
        {
            await DbSet.AddAsync(entity).ConfigureAwait(false);
        }

        public virtual Task DeleteAsync(TEntity entity)
        {
            DbSet.Remove(entity);
            return Task.CompletedTask;
        }

        public virtual async Task<List<TEntity>> GetAllAsync()
        {
            var objList = await DbSet.ToListAsync();
            return objList;
        }

        public virtual async Task<TEntity> GetByIdAsync(int entityId)
        {
            return await DbSet.FindAsync(entityId).ConfigureAwait(false);
        }

        public virtual async Task SaveChanges()
        {
            await DbContext.SaveChangesAsync();
        }

        public virtual async Task UpdateAsync(TEntity entity, TEntity olderEntity)
        {
            DbContext.Entry(olderEntity).CurrentValues.SetValues(entity);
        }
    }
}
