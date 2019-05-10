using FluentValidation;
using FluentValidation.Results;
using MMWebAPI.Domain.Exceptions;
using MMWebAPI.Domain.Interfaces.Domain;
using MMWebAPI.Domain.Interfaces.Repository;
using MMWebAPI.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValidationException = MMWebAPI.Domain.Exceptions.ValidationException;

namespace MMWebAPI.Domain.Implementations.Domain
{
    public class DomainServiceBase<TEntity> : IDomainServiceBase<TEntity> 
        where TEntity : Entity
    {
        private readonly IRepositoryBase<TEntity> _repo;
        AbstractValidator<TEntity> _validator;

        public DomainServiceBase(IRepositoryBase<TEntity> repo, AbstractValidator<TEntity> validator)
        {
            _validator = validator;
            _repo = repo;
        }
        public async Task<TEntity> AddAsync(TEntity entity)
        {
            var validationResult = _validator.Validate(entity);
            if (! validationResult.IsValid)
                throw new ValidationException(this.ValidationResultToString(validationResult));

            await _repo.AddAsync(entity);
            await _repo.SaveChanges();
            return entity;
        }

        public async Task DeleteAsync(int entityId)
        {
            var entity = await this.GetByIdAsync(entityId);
            await _repo.DeleteAsync(entity);
            await _repo.SaveChanges();
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            var result = await _repo.GetAllAsync();
            if (!result.Any())
                throw new NoContentException();
            return result;
        }

        public async Task<TEntity> GetByIdAsync(int entityId)
        {
            var result = await _repo.GetByIdAsync(entityId);
            if (result == null)
            {
                throw new NotFoundException();
            }
            return result;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity, int entityId)
        {
            entity.Id = entityId;
            var olderEntity = await this.GetByIdAsync(entityId);
            await _repo.UpdateAsync(entity,olderEntity);
            await _repo.SaveChanges();
            return entity;
        }

        private string ValidationResultToString(ValidationResult validationResult) {
            var sb = new StringBuilder();
            validationResult.Errors.ToList().ForEach(x => {
                sb.Append(x.ErrorMessage);
            });
            return sb.ToString();
        }
    }
}
