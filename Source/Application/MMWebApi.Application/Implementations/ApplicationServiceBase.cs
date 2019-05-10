using AutoMapper;
using MMWebAPI.Application.Interfaces;
using MMWebAPI.Domain.Interfaces.Domain;
using MMWebAPI.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MMWebAPI.Application.Implementations
{
    public class ApplicationServiceBase<TEntity, TRequest, TResponse> : IApplicationServiceBase<TRequest, TResponse>
    where TEntity : Entity
    where TRequest : class
    where TResponse : class
    {
        private readonly IDomainServiceBase<TEntity> _domain;
        private readonly IMapper _mapper;
        public ApplicationServiceBase(IDomainServiceBase<TEntity> domain, IMapper mapper)
        {
            _domain = domain;
            _mapper = mapper;
        }

        public async Task<TResponse> AddAsync(TRequest request)
        {
            var entity = _mapper.Map<TEntity>(request);
            var entityAdded = await _domain.AddAsync(entity);
            var responseEntity = _mapper.Map<TResponse>(entityAdded);
            return responseEntity;
        }

        public async Task DeleteAsync(int entityId)
        {
            await _domain.DeleteAsync(entityId);
        }

        public async Task<List<TResponse>> GetAllAsync()
        {
            var databaseList = await _domain.GetAllAsync();
            var responseList = _mapper.Map<List<TResponse>>(databaseList);
            return responseList;
        }

        public async Task<TResponse> GetByIdAsync(int entityId)
        {
            var databaseEntity =  await _domain.GetByIdAsync(entityId);
            var response = _mapper.Map<TResponse>(databaseEntity);
            return response;
        }

        public async Task<TResponse> UpdateAsync(TRequest request, int entityId)
        {
            var entity = _mapper.Map<TEntity>(request);
            var updatedEntity = await _domain.UpdateAsync(entity, entityId);
            var response = _mapper.Map<TResponse>(updatedEntity);
            return response;
        }
    }
}
