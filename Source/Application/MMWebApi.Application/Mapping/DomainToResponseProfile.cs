using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using MMWebAPI.Application.InOut.FinancialTransaction;
using MMWebAPI.Application.InOut.FinancialTransactionGroup;
using MMWebAPI.Domain.Models;
namespace MMWebAPI.Application.Mapping
{
    public class DomainToResponseProfile : Profile
    {
        public DomainToResponseProfile()
        {
            CreateMap<FinancialTransaction, FinancialTransactionResponse>();
            CreateMap<FinancialTransactionGroup, FinancialTransactionGroupResponse>();
        }
    }
}
