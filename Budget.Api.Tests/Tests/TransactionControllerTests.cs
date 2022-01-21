using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Budget.Api.Tests.Utilities;
using Budget.Shared;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace Budget.Api.Tests.Tests;

public class TransactionControllerTests : DbContextTests
{
    private readonly TransactionController _controller;

    private TransactionControllerTests()
    {
        _controller = new TransactionController(_actContext);
    }

    public class GetTransactions : TransactionControllerTests
    {
        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(5)]
        public async Task gets_all_transactions(int countOfTransactions)
        {
            for (var i = 0; i < countOfTransactions; i++)
                ArrangeTransaction();

            var response = await _controller.GetTransactions();

            Assert.Equal(countOfTransactions, response.Value?.LongCount());
        }

        [Fact]
        public async Task oldest_transactions_last()
        {
            var year = 2021;
            var olderTransactionCategory = 151;
            ArrangeTransaction(category: 151, year: year);
            ArrangeTransaction(year: year + 1);

            var response = await _controller.GetTransactions();

            var lastTransaction = response.Value.Last();
            Assert.Equal(olderTransactionCategory, lastTransaction.CategoryId);
        }

        [Fact]
        public async Task includes_bank_account()
        {
            ArrangeTransaction(account: 1);

            var response = await _controller.GetTransactions();

            var transaction = Assert.Single(response?.Value);
            Assert.NotNull(transaction.Account);
        }

        [Fact]
        public async Task includes_category()
        {
            ArrangeTransaction(category: 1);

            var response = await _controller.GetTransactions();

            var transaction = Assert.Single(response?.Value);
            Assert.NotNull(transaction.Category);
        }

        [Fact]
        public async Task includes_tag()
        {
            ArrangeTransaction(tag: 1);

            var response = await _controller.GetTransactions();

            var transaction = Assert.Single(response?.Value);
            Assert.NotNull(transaction.Tag);
        }

        [Fact]
        public async Task gets_transactions_with_year()
        {
            var year = 2019;
            ArrangeTransaction(year: year);

            var response = await _controller.GetTransactions(year: year);

            Assert.Single(response.Value);
        }

        [Fact]
        public async Task gets_transactions_with_month()
        {
            var month = 9;
            ArrangeTransaction(month: month);

            var response = await _controller.GetTransactions(month: month);

            Assert.Single(response.Value);
        }

        [Fact]
        public async Task gets_transactions_with_category()
        {
            var category = 9;
            ArrangeTransaction(category: category);

            var response = await _controller.GetTransactions(category: category);

            Assert.Single(response.Value);
        }

        [Fact]
        public async Task gets_transactions_with_account()
        {
            var account = 9;
            ArrangeTransaction(account: account);

            var response = await _controller.GetTransactions(account: account);

            Assert.Single(response.Value);
        }

        [Fact]
        public async Task gets_transactions_with_tag()
        {
            var tag = 9;
            ArrangeTransaction(tag: tag);

            var response = await _controller.GetTransactions(tag: tag);

            Assert.Single(response.Value);
        }

        [Fact]
        public async Task does_not_get_transactions_with_in_wrong_year()
        {
            var year = 2019;
            ArrangeTransaction(year: year);

            var response = await _controller.GetTransactions(year: year+1);

            Assert.Empty(response.Value);
        }

        [Fact]
        public async Task does_not_get_transactions_with_in_wrong_month()
        {
            var month = 9;
            ArrangeTransaction(month: month);

            var response = await _controller.GetTransactions(month: month+1);

            Assert.Empty(response.Value);
        }

        [Fact]
        public async Task does_not_get_transactions_with_in_wrong_category()
        {
            var category = 9;
            ArrangeTransaction(category: category);

            var response = await _controller.GetTransactions(category: category+1);

            Assert.Empty(response.Value);
        }

        [Fact]
        public async Task does_not_get_transactions_with_in_wrong_account()
        {
            var account = 9;
            ArrangeTransaction(account: account);

            var response = await _controller.GetTransactions(account: account+1);

            Assert.Empty(response.Value);
        }

        [Fact]
        public async Task does_not_get_transactions_with_in_wrong_tag()
        {
            var tag = 9;
            ArrangeTransaction(tag: tag);

            var response = await _controller.GetTransactions(tag: tag+1);

            Assert.Empty(response.Value);
        }

        private void ArrangeTransaction(int? year = null, int? month = null, long account = 1, long category = 1, long tag = 1)
        {
            if (_arrangeContext.BankAccounts.Find(account) == null)
                _arrangeContext.BankAccounts.Add(new()
                {
                    Id = account,
                    Color = ""
                });
            if (_arrangeContext.Categories.Find(category) == null)
                _arrangeContext.Categories.Add(new()
                {
                    Id = category,
                    Color = ""
                });
            if (_arrangeContext.Tags.Find(tag) == null)
                _arrangeContext.Tags.Add(new()
                {
                    Id = tag,
                    Color = ""
                });
            _arrangeContext.Transactions.Add(new()
            {
                AccountId = account,
                CategoryId = category,
                TagId = tag,
                When = new DateTime(year ?? 2021, month ?? 1, 1)
            });
            _arrangeContext.SaveChanges();
        }
    }

    public class GetTransaction : TransactionControllerTests
    {
        [Fact]
        public async Task when_no_transaction_returns_not_found()
        {
            var response = await _controller.GetTransaction(150);

            Assert.Equal((int)HttpStatusCode.NotFound, (response.Result as StatusCodeResult)?.StatusCode);
        }

        [Fact]
        public async Task does_not_get_wrong_transaction()
        {
            var id = _arrangeContext.Transactions.Add(new Transaction()).Entity.Id;
            await _arrangeContext.SaveChangesAsync();

            var response = await _controller.GetTransaction(id + 1);

            Assert.Null(response.Value);
        }

        [Fact]
        public async Task gets_transaction()
        {
            var amount = 5.00m;
            var id = _arrangeContext.Transactions.Add(new Transaction{Amount = amount}).Entity.Id;
            await _arrangeContext.SaveChangesAsync();

            var response = await _controller.GetTransaction(id);

            Assert.Equal(amount, response.Value?.Amount);
        }
    }
}