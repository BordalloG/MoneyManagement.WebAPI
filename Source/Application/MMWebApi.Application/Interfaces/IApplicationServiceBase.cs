using MMWebAPI.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MMWebAPI.Application.Interfaces
{
    public interface IApplicationServiceBase<TRequest, TResponse>
        where TRequest : class
        where TResponse : class
    {
        Task<TResponse> AddAsync(TRequest request);
        Task DeleteAsync(int entityId);
        Task<TResponse> GetByIdAsync(int entityId);
        Task<List<TResponse>> GetAllAsync();
        Task<TResponse> UpdateAsync(TRequest request, int entityId);
    }
}
