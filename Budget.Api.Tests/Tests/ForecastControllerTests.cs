using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Budget.Api.Tests.Utilities;
using Budget.Shared;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace Budget.Api.Tests.Tests;

public class ForecastControllerTests : DbContextTests
{
    private readonly ForecastController _controller;

    protected ForecastControllerTests()
    {
        _controller = new ForecastController(_actContext);
    }

    public class GetForecastTest : ForecastControllerTests
    {
        [Fact]
        public async Task returns_not_found_when_no_account()
        {
            var response = await _controller.GetForecastTest(0);

            Assert.Equal((int)HttpStatusCode.NotFound, (response.Result as StatusCodeResult)?.StatusCode);
        }

        [Fact]
        public async Task includes_forecast()
        {
            var amount = 1123;
            var year = 2021;
            var month = 3;
            var id = 14;
            ArrangeForecast(year: year, month: month, id: id, amount: amount);
            await _arrangeContext.SaveChangesAsync();

            var response = await _controller.GetForecastTest(id);

            Assert.Equal(year, response.Value?.Year);
            Assert.Equal(month, response.Value?.Month);
            Assert.Equal(amount, response.Value?.ForecastedAmount);
        }

        [Fact]
        public async Task includes_category()
        {
            var forecastId = 14;
            var categoryId = 4L;
            var name = "Food";
            var color = "#FFFFFF";
            ArrangeForecast(id: forecastId, category: categoryId);
            _arrangeContext.Categories.Find(categoryId).Name = name;
            _arrangeContext.Categories.Find(categoryId).Color = color;
            await _arrangeContext.SaveChangesAsync();

            var response = await _controller.GetForecastTest(forecastId);

            Assert.Equal(name, response.Value?.CategoryName);
            Assert.Equal(color, response.Value?.CategoryColor);
        }

        [Theory]
        [InlineData()]
        [InlineData(100)]
        [InlineData(100, 10)]
        [InlineData(100, 50, 50)]
        public async Task includes_transaction_total(params int[] transactionAmounts)
        {
            var year = 2022;
            var month = 6;
            var categoryId = 7;
            var forecastId = 14;
            ArrangeForecast(month: month, year: year, category: categoryId, id: forecastId);
            foreach (var amount in transactionAmounts)
                ArrangeTransaction(month: month, year: year, amount: amount, category: categoryId);
            await _arrangeContext.SaveChangesAsync();

            var response = await _controller.GetForecastTest(forecastId);

            Assert.Equal(transactionAmounts.Sum(), response.Value?.SpentAmount);
        }

        [Fact]
        public async Task does_not_sum_transaction_in_wrong_month()
        {
            var total = 100;
            var month = 4;
            var forecastId = 15;
            ArrangeForecast(id: forecastId, month: month, amount: total);
            ArrangeTransaction(amount: total+1, month: month+1);

            var response = await _controller.GetForecastTest(forecastId);

            Assert.True(response.Value?.SpentLessThanForecasted);
        }

        [Fact]
        public async Task does_not_sum_transaction_in_wrong_year()
        {
            var total = 100;
            var year = 2022;
            var forecastId = 15;
            ArrangeForecast(id: forecastId, year: year, amount: total);
            ArrangeTransaction(amount: total+1, year: year+1);

            var response = await _controller.GetForecastTest(forecastId);

            Assert.True(response.Value?.SpentLessThanForecasted);
        }

        [Fact]
        public async Task does_not_sum_transaction_in_wrong_category()
        {
            var total = 100;
            var forecastId = 4;
            ArrangeForecast(id: forecastId, amount: total);

            var response = await _controller.GetForecastTest(forecastId);

            Assert.True(response.Value?.SpentLessThanForecasted);
        }

        private void ArrangeForecast(long id = 1, int month = 1, int year = 2021, long category = 1, decimal amount = 1)
        {
            if (_arrangeContext.Categories.Find(category) == null)
                _arrangeContext.Categories.Add(new()
                {
                    Id = category,
                    Color = ""
                });
            _arrangeContext.Forecasts.Add(new Forecast
            {
                Id = id,
                Month = month,
                Year = year,
                CategoryId = category,
                Amount = amount
            });
            _arrangeContext.SaveChanges();
        }

        private void ArrangeTransaction(int month = 1, int year = 2021, decimal amount = 1, long category = 1)
        {
            if (_arrangeContext.Categories.Find(category) == null)
                _arrangeContext.Categories.Add(new()
                {
                    Id = category,
                    Color = ""
                });
            _arrangeContext.Transactions.Add(new()
            {
                When = new DateTime(year, month, 1),
                Amount = amount,
                CategoryId = category
            });
            _arrangeContext.SaveChanges();
        }
    }

    public class GetForecastTests : ForecastControllerTests
    {
    }
}