using System;
using System.Collections.Generic;
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

            Assert.Equal(0, response.Value?.SpentAmount);
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

            Assert.Equal(0, response.Value?.SpentAmount);
        }

        [Fact]
        public async Task does_not_sum_transaction_in_wrong_category()
        {
            var forecastId = 4;
            var categoryId = 3;
            var total = 100;
            ArrangeForecast(id: forecastId, category: categoryId, amount: total);
            ArrangeTransaction(category: categoryId+1, amount: total+1);

            var response = await _controller.GetForecastTest(forecastId);

            Assert.Equal(0, response.Value?.SpentAmount);
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
        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        public async Task returns_proper_number_of_tests(int countOfForecasts)
        {
            for (var i = 0; i < countOfForecasts; i++)
                ArrangeForecast();

            var response = GetForecastTestsViaController();

            Assert.Equal(countOfForecasts, response.Count());
        }

        [Fact]
        public async Task includes_forecast()
        {
            var amount = 1123;
            var year = 2021;
            var month = 3;
            ArrangeForecast(year: year, month: month, amount: amount);
            await _arrangeContext.SaveChangesAsync();

            var response = GetForecastTestsViaController(year: year, month: month);

            var test = response.First();
            Assert.Equal(year, test.Year);
            Assert.Equal(month, test.Month);
            Assert.Equal(amount, test.ForecastedAmount);
        }

        [Fact]
        public async Task includes_category()
        {
            var categoryId = 4L;
            var name = "Food";
            var color = "#FFFFFF";
            ArrangeForecast(category: categoryId);
            _arrangeContext.Categories.Find(categoryId).Name = name;
            _arrangeContext.Categories.Find(categoryId).Color = color;
            await _arrangeContext.SaveChangesAsync();

            var response = GetForecastTestsViaController();

            var test = response.First();
            Assert.Equal(name, test.CategoryName);
            Assert.Equal(color, test.CategoryColor);
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
            ArrangeForecast(month: month, year: year, category: categoryId);
            foreach (var amount in transactionAmounts)
                ArrangeTransaction(month: month, year: year, amount: amount, category: categoryId);
            await _arrangeContext.SaveChangesAsync();

            var response = GetForecastTestsViaController(year: year, month: month);

            var test = response.First();
            Assert.Equal(transactionAmounts.Sum(), test.SpentAmount);
        }

        [Fact]
        public async Task does_not_forecast_in_wrong_month()
        {
            var month = 4;
            ArrangeForecast(month: month+1);

            var response = GetForecastTestsViaController(month: month);

            Assert.Empty(response);
        }

        [Fact]
        public async Task does_not_forecast_in_wrong_year()
        {
            var year = 2020;
            ArrangeForecast(year: year+1);

            var response = GetForecastTestsViaController(year: year);

            Assert.Empty(response);
        }

        [Fact]
        public async Task does_not_sum_transaction_in_wrong_month()
        {
            var total = 100;
            var month = 4;
            ArrangeForecast(month: month, amount: total);
            ArrangeTransaction(amount: total + 1, month: month + 1);

            var response = GetForecastTestsViaController(month: month);

            var test = response.First();
            Assert.Equal(0, test.SpentAmount);
        }

        [Fact]
        public async Task does_not_sum_transaction_in_wrong_year()
        {
            var total = 100;
            var year = 2022;
            ArrangeForecast(year: year, amount: total);
            ArrangeTransaction(amount: total + 1, year: year + 1);

            var response = GetForecastTestsViaController(year: year);

            var test = response.First();
            Assert.Equal(0, test.SpentAmount);
        }

        [Fact]
        public async Task does_not_sum_transaction_in_wrong_category()
        {
            var categoryId = 3;
            var total = 100;
            ArrangeForecast(category: categoryId);
            ArrangeTransaction(category: categoryId+1, amount: total);

            var response = GetForecastTestsViaController();

            var test = response.First();
            Assert.Equal(0, test.SpentAmount);
        }

        private IEnumerable<ForecastTest> GetForecastTestsViaController(int year = 2021, int month = 1)
        {
            return _controller.GetForecastTests(year, month).Result.Value;
        }

        private void ArrangeForecast(int month = 1, int year = 2021, long category = 1, decimal amount = 1)
        {
            if (_arrangeContext.Categories.Find(category) == null)
                _arrangeContext.Categories.Add(new()
                {
                    Id = category,
                    Color = ""
                });
            _arrangeContext.Forecasts.Add(new Forecast
            {
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
}