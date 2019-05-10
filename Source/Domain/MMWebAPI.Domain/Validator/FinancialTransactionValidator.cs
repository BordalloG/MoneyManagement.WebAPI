using FluentValidation;
using MMWebAPI.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MMWebAPI.Domain.Validator
{
    public class FinancialTransactionValidator : AbstractValidator<FinancialTransaction>
    {
        public FinancialTransactionValidator()
        {
            RuleFor(ft => ft.Title).NotEmpty().MaximumLength(200);
            RuleFor(ft => ft.Description).MaximumLength(200);
            RuleFor(ft => ft.Value).NotEmpty();
            RuleFor(ft => ft.TransactionDate).NotEmpty();
        }
    }
}
