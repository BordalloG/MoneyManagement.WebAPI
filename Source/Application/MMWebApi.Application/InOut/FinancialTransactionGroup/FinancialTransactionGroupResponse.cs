using MMWebAPI.Application.InOut.FinancialTransaction;
using System;
using System.Collections.Generic;
using System.Text;

namespace MMWebAPI.Application.InOut.FinancialTransactionGroup
{
    public class FinancialTransactionGroupResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<FinancialTransactionResponse> FinancialTransactions { get; set; }
        public double TotalValue { get; set; }
    }
}
