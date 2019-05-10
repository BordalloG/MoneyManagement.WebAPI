using FluentValidation;
using MMWebAPI.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MMWebAPI.Domain.Validator
{
    public class FinancialTransactionGroupValidator : AbstractValidator<FinancialTransactionGroup>
    {
        public FinancialTransactionGroupValidator()
        {
            RuleFor(ftg => ftg.Title).NotEmpty().MaximumLength(200);
            RuleFor(ftg => ftg.Description).MaximumLength(200);
            RuleForEach(ftg => ftg.FinancialTransactions)
                .SetValidator(new FinancialTransactionValidator());
        }
    }
}
