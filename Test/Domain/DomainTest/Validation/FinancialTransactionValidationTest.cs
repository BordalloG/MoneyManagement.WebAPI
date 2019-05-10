using MMWebAPI.Domain.Models;
using MMWebAPI.Domain.Validator;
using System;
using Xunit;
namespace DomainTest.Validation
{
    public class FinancialTransactionValidationTest
    {
        [Theory]
        [InlineData("Title", "Description", 200 )]
        [InlineData("Title with 200 characters Lorem ipsum dolor sit amet Lorem ipsum dolor sit amet Lorem ipsum dolor sit amet Lorem ipsum dolor sit amet Lorem ipsum dolor sit amet Lorem ipsum dolor sit amet Lorem a a aa", "Description", 200)]
        [InlineData("Title", "Description with 200 characters Lorem ipsum dolor sit amet Lorem ipsum dolor sit amet Lorem ipsum dolor sit amet Lorem ipsum dolor sit amet Lorem ipsum dolor sit amet Lorem ipsum dolor sit amet Lorema", 200)]
        [InlineData("Title", " ", 200.65)]
        [InlineData("Title", "    ", 20)]
        [InlineData("Title", null, 1000)]
        [InlineData("Title", "", 1)]
        public void FinancialTransactionShouldBeValid(string title, string description, double value)
        {
            var transactionDate = DateTime.Today;

            var financialTransaction = new FinancialTransaction(title,description,value,transactionDate);
            var financialValidator = new FinancialTransactionValidator();
            var validationResult = financialValidator.Validate(financialTransaction);

            Assert.True(validationResult.IsValid);
            Assert.Empty(validationResult.Errors);
        }
        [Theory]
        [InlineData("", "", 1)]
        [InlineData(" ", "", 0)]
        [InlineData(null, "", 0)]
        [InlineData("Title with 201 characters Lorem ipsum dolor sit amet Lorem ipsum dolor sit amet Lorem ipsum dolor sit amet Lorem ipsum dolor sit amet Lorem ipsum dolor sit amet Lorem ipsum dolor sit amet Lorem a a aaa", "Description", 200)]
        [InlineData("Title", "Description with 201 characters Lorem ipsum dolor sit amet Lorem ipsum dolor sit amet Lorem ipsum dolor sit amet Lorem ipsum dolor sit amet Lorem ipsum dolor sit amet Lorem ipsum dolor sit amet Loremas", 200)]
        [InlineData("Title", "", 0)]

        public void FinancialTransactionShouldNotBeValid(string title, string description, double value)
        {
            var transactionDate = DateTime.Today;

            var financialTransaction = new FinancialTransaction(title, description, value, transactionDate);
            var financialValidator = new FinancialTransactionValidator();
            var validationResult = financialValidator.Validate(financialTransaction);

            Assert.False(validationResult.IsValid);
            Assert.NotEmpty(validationResult.Errors);
        }
    }
}
