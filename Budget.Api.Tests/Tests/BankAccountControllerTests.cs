using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Budget.Api.Tests.Utilities;
using Budget.Shared;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace Budget.Api.Tests.Tests;

public class BankAccountControllerTests : BudgetContextTests
{
    private readonly BankAccountController _controller;

    protected BankAccountControllerTests()
    {
        _controller = new BankAccountController(_actContext);
    }

    public class GetBankAccountTest : BankAccountControllerTests
    {
        [Fact]
        public async Task returns_not_found_when_no_account()
        {
            var response = await _controller.GetBankAccountTest(0);

            Assert.Equal((int)HttpStatusCode.NotFound, (response.Result as StatusCodeResult)?.StatusCode);
        }

        [Fact]
        public async Task includes_account()
        {
            var name = "Broken Cistern";
            var color = "red";
            var live = 301;
            var id = _arrangeContext.BankAccounts.Add(new BankAccount
            {
                Name = name,
                Color = color,
                LiveTotal = live
            }).Entity.Id;
            await _arrangeContext.SaveChangesAsync();

            var response = await _controller.GetBankAccountTest(id);

            Assert.Equal(name, response.Value?.Name);
            Assert.Equal(color, response.Value?.Color);
            Assert.Equal(live, response.Value?.ExpectedTotal);
        }

        [Theory]
        [InlineData(100, 100)]
        [InlineData(100, 50, 50)]
        public async Task actual_total_sums_transactions(int total, params int[] transactionAmounts)
        {
            var name = "Broken Cistern";
            var id = _arrangeContext.BankAccounts.Add(new BankAccount
            {
                Name = name,
                Color = "",
                LiveTotal = total
            }).Entity.Id;
            foreach (var amount in transactionAmounts)
                _arrangeContext.Transactions.Add(new Transaction
                {
                    AccountId = id,
                    Amount = amount
                });
            await _arrangeContext.SaveChangesAsync();

            var response = await _controller.GetBankAccountTest(id);

            Assert.Equal(total, response.Value?.ActualTotal);
        }

        [Fact]
        public async Task does_not_sum_wrong_transactions()
        {
            var total = 100;
            var id = _arrangeContext.BankAccounts.Add(new BankAccount
            {
                Color = "",
                LiveTotal = total
            }).Entity.Id;
            _arrangeContext.Transactions.Add(new Transaction
            {
                AccountId = id+1,
                Amount = total
            });
            await _arrangeContext.SaveChangesAsync();

            var response = await _controller.GetBankAccountTest(id);

            Assert.Equal(0, response.Value?.ActualTotal);
        }
    }

    public class GetBankAccountTests : BankAccountControllerTests
    {
        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(5)]
        public async Task creates_proper_number_of_tests(int count)
        {
            for (var i = 0; i < count; i++)
                _arrangeContext.BankAccounts.Add(new BankAccount{Color = ""});
            await _arrangeContext.SaveChangesAsync();

            var response = await _controller.GetBankAccountTests();

            Assert.Equal(count, response.Value?.Count());
        }

        [Fact]
        public async Task includes_the_account()
        {
            var name = "Life Savings";
            var color = "blue";
            var live = 103;
            _arrangeContext.BankAccounts.Add(new BankAccount{Name = name, Color = color, LiveTotal = live});
            await _arrangeContext.SaveChangesAsync();

            var response = await _controller.GetBankAccountTests();

            var test = response.Value?.First();
            Assert.Equal(name, test.Name);
            Assert.Equal(color, test.Color);
            Assert.Equal(live, test.ExpectedTotal);
        }

        [Theory]
        [InlineData(100, 100)]
        [InlineData(100, 50, 50)]
        public async Task actual_total_sums_transactions(int total, params int[] transactionAmounts)
        {
            var name = "Broken Cistern";
            var id = _arrangeContext.BankAccounts.Add(new BankAccount
            {
                Name = name,
                Color = "",
                LiveTotal = total
            }).Entity.Id;
            foreach (var amount in transactionAmounts)
                _arrangeContext.Transactions.Add(new Transaction
                {
                    AccountId = id,
                    Amount = amount
                });
            await _arrangeContext.SaveChangesAsync();

            var response = await _controller.GetBankAccountTests();

            var test = response.Value?.First();
            Assert.Equal(total, test?.ActualTotal);
        }

        [Fact]
        public async Task does_not_sum_wrong_transactions()
        {
            var total = 100;
            var id = _arrangeContext.BankAccounts.Add(new BankAccount
            {
                Color = "",
                LiveTotal = total
            }).Entity.Id;
            _arrangeContext.Transactions.Add(new Transaction
            {
                AccountId = id+1,
                Amount = total
            });
            await _arrangeContext.SaveChangesAsync();

            var response = await _controller.GetBankAccountTests();

            var test = response.Value?.First();
            Assert.Equal(0, test?.ActualTotal);
        }
    }

    public class GetBankAccounts : BankAccountControllerTests
    {
        [Fact]
        public void accounts_are_alphabetized()
        {
            var first = "A";
            var second = "B";
            _assertContext.BankAccounts.Add(new BankAccount
            {
                Name = second, Color = ""
            });
            _assertContext.BankAccounts.Add(new BankAccount()
            {
                Name = first, Color = ""
            });
            _assertContext.SaveChanges();

            var accounts = _controller.GetBankAccounts().Result.Value;

            var account = accounts.First();
            Assert.Equal(first, account.Name);
        }
    }
}