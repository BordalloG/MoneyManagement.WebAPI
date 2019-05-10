using MMWebAPI.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MMWebAPI.Domain.Interfaces.Repository
{
    public interface IRepositoryBase<TEntity> where TEntity : Entity
    {
        Task<List<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(int entityId);
        Task AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity, TEntity olderEntity);
        Task DeleteAsync(TEntity entity);
        Task SaveChanges();
    }
}
