using System;

namespace MMWebAPI.Domain.Models
{
    public class FinancialTransaction : Entity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public double Value { get; set; }
        public DateTime TransactionDate { get; set; }
        public FinancialTransactionGroup Group { get; set; }
        public int GroupId { get; set; }

        public FinancialTransaction()
        {

        }
        public FinancialTransaction(int id, string title, string description, double value, DateTime transactionDate) : base(id)
        {
            Title = title;
            Description = description;
            Value = value;
            TransactionDate = transactionDate;
        }

        public FinancialTransaction(string title, string description, double value, DateTime transactionDate)
        {
            Title = title;
            Description = description;
            Value = value;
            TransactionDate = transactionDate;
        }
    }
}
