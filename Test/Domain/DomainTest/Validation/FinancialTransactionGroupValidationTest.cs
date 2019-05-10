using MMWebAPI.Domain.Models;
using MMWebAPI.Domain.Validator;
using System;
using Xunit;

namespace DomainTest.Validation
{
    public class FinancialTransactionGroupValidationTest
    {
        [Theory]
        [InlineData("Title","Description")]
        [InlineData("Title", "")]
        [InlineData("Title with 200 characters Lorem ipsum dolor sit amet Lorem ipsum dolor sit amet Lorem ipsum dolor sit amet Lorem ipsum dolor sit amet Lorem ipsum dolor sit amet Lorem ipsum dolor sit amet Lorem a a aa", "Description")]
        [InlineData("Title", "Description with 200 characters Lorem ipsum dolor sit amet Lorem ipsum dolor sit amet Lorem ipsum dolor sit amet Lorem ipsum dolor sit amet Lorem ipsum dolor sit amet Lorem ipsum dolor sit amet Lorema")]
        public void FinancialTransactionGroupShouldBeValid(string title, string description)
        {
            var group = new FinancialTransactionGroup(title,description);
            var validator = new FinancialTransactionGroupValidator();

            var validationResult = validator.Validate(group);

            Assert.True(validationResult.IsValid);
            Assert.Empty(validationResult.Errors);
        }

        [Theory]
        [InlineData("Title", "Description",200)]
        [InlineData("Title", "",200)]
        [InlineData("Title with 200 characters Lorem ipsum dolor sit amet Lorem ipsum dolor sit amet Lorem ipsum dolor sit amet Lorem ipsum dolor sit amet Lorem ipsum dolor sit amet Lorem ipsum dolor sit amet Lorem a a aa", "Description",200)]
        [InlineData("Title", "Description with 200 characters Lorem ipsum dolor sit amet Lorem ipsum dolor sit amet Lorem ipsum dolor sit amet Lorem ipsum dolor sit amet Lorem ipsum dolor sit amet Lorem ipsum dolor sit amet Lorema",200)]
        public void FinancialTransactionsShouldBeValid(string title, string description, double value)
        {
            var group = new FinancialTransactionGroup("Title", "Description");
            group.FinancialTransactions.Add(new FinancialTransaction(title, description, value, DateTime.Now));
            var validator = new FinancialTransactionGroupValidator();

            var validationResult = validator.Validate(group);

            Assert.True(validationResult.IsValid);
            Assert.Empty(validationResult.Errors);
        }


        [Theory]
        [InlineData("", "Description")]
        [InlineData("  ", "")]
        [InlineData(null, "")]
        [InlineData("Title with 201 characters Lorem ipsum dolor sit amet Lorem ipsum dolor sit amet Lorem ipsum dolor sit amet Lorem ipsum dolor sit amet Lorem ipsum dolor sit amet Lorem ipsum dolor sit amet Lorem a a aaa", "Description")]
        [InlineData("Title", "Description with 201 characters Lorem ipsum dolor sit amet Lorem ipsum dolor sit amet Lorem ipsum dolor sit amet Lorem ipsum dolor sit amet Lorem ipsum dolor sit amet Lorem ipsum dolor sit amet Loremaa")]
        public void FinancialTransactionGroupShouldNotBeValid(string title, string description)
        {
            var group = new FinancialTransactionGroup(title, description);
            var validator = new FinancialTransactionGroupValidator();

            var validationResult = validator.Validate(group);

            Assert.False(validationResult.IsValid);
            Assert.NotEmpty(validationResult.Errors);

        }

        [Theory]
        [InlineData("", "", 1)]
        [InlineData(" ", "", 0)]
        [InlineData(null, "", 0)]
        [InlineData("Title with 201 characters Lorem ipsum dolor sit amet Lorem ipsum dolor sit amet Lorem ipsum dolor sit amet Lorem ipsum dolor sit amet Lorem ipsum dolor sit amet Lorem ipsum dolor sit amet Lorem a a aaa", "Description", 200)]
        [InlineData("Title", "Description with 201 characters Lorem ipsum dolor sit amet Lorem ipsum dolor sit amet Lorem ipsum dolor sit amet Lorem ipsum dolor sit amet Lorem ipsum dolor sit amet Lorem ipsum dolor sit amet Loremas", 200)]
        [InlineData("Title", "", 0)]
        public void FinancialTransactionsShouldNotBeValid(string title, string description, double value)
        {
            var group = new FinancialTransactionGroup("Title", "Description");
            group.FinancialTransactions.Add(new FinancialTransaction(title, description, value, DateTime.Now));
            var validator = new FinancialTransactionGroupValidator();

            var validationResult = validator.Validate(group);

            Assert.False(validationResult.IsValid);
            Assert.NotEmpty(validationResult.Errors);

        }

    }
}
