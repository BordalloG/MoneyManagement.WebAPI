using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MMWebAPI.Domain.Models
{
    public class FinancialTransactionGroup : Entity
    {
        protected double _totalValue;
        public string Title { get; set; }
        public string Description { get; set; }
        public List<FinancialTransaction> FinancialTransactions { get; set; }
        public double TotalValue
        {
            get
            {
                if(FinancialTransactions != null)
                    return FinancialTransactions.Sum(v => v.Value);
                return _totalValue;
            }
            set
            {
                _totalValue = value;
            }
        }

        public FinancialTransactionGroup()
        {

        }
        public FinancialTransactionGroup(string title, string description, List<FinancialTransaction> financialTransactions)
        {
            Title = title;
            Description = description;
            FinancialTransactions = financialTransactions.Any() ? financialTransactions : new List<FinancialTransaction>();
        }
        public FinancialTransactionGroup(int id, string title, string description, List<FinancialTransaction> financialTransactions ) : base (id)
        {
            Title = title;
            Description = description;
            FinancialTransactions = financialTransactions.Any() ? financialTransactions : new List<FinancialTransaction>();
        }

        public FinancialTransactionGroup(string title, string description)
        {
            Title = title;
            Description = description;
            FinancialTransactions = new List<FinancialTransaction>();
        }
    }
}
