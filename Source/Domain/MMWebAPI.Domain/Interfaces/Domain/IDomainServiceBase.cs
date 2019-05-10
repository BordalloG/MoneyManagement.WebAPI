using FluentValidation;
using MMWebAPI.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MMWebAPI.Domain.Interfaces.Domain
{
    public interface IDomainServiceBase<TEntity>
        where TEntity : Entity
    {
        Task<List<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(int entityId);
        Task<TEntity> AddAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity, int entityId);
        Task DeleteAsync(int entityId);
    }
}
