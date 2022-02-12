using System;
using Xunit;

namespace Budget.Shared.Tests;

public class ApiConstantsTests
{
    public class GetTestsPath : ApiConstantsTests
    {
        [Fact]
        public void has_correct_endpoint()
        {
            Assert.StartsWith("api/Forecast/Test", ApiConstants.Forecast.GetTestsPath(new DateTime()));
        }

        [Fact]
        public void sets_year_and_month_query_parameters()
        {
            var year = 2022;
            var month = 2;

            var endpoint = ApiConstants.Forecast.GetTestsPath(new DateTime(year, month, 1));

            Assert.EndsWith($"?year={year}&month={month}", endpoint);
        }
    }
}