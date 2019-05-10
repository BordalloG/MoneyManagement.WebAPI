using AutoMapper;
using MMWebAPI.Domain.Models;
using MMWebAPI.Application.InOut.FinancialTransaction;
using MMWebAPI.Application.InOut.FinancialTransactionGroup;
namespace MMWebAPI.Application.Mapping
{
    public class RequestToDomainProfile : Profile
    {
        public RequestToDomainProfile()
        {
            CreateMap<FinancialTransactionRequest, FinancialTransaction>();
            CreateMap<FinancialTransactionGroupRequest, FinancialTransactionGroup>();
        }
    }
}
