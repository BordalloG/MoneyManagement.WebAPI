using System;
using System.Collections.Generic;
using System.Text;

namespace MMWebAPI.Application.InOut.FinancialTransaction
{
    public class FinancialTransactionResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double Value { get; set; }
        public DateTime TransactionDate { get; set; }
        public int GroupId { get; set; }
    }
}
