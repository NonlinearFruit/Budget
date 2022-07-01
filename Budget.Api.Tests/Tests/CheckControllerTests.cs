using System.Linq;
using System.Threading.Tasks;
using Budget.Api.Tests.Utilities;
using Budget.Shared;
using Xunit;

namespace Budget.Api.Tests.Tests;

public class CheckControllerTests : BudgetContextTests
{
    private readonly CheckController _controller;

    public CheckControllerTests()
    {
        _controller = new CheckController(_actContext);
    }

    public class GetChecks : CheckControllerTests
    {
        [Fact]
        public async Task sort_by_check_number()
        {
            var firstNumber = 1;
            var secondNumber = 2;
            _arrangeContext.Checks.Add(new Check { CheckNumber = secondNumber });
            _arrangeContext.Checks.Add(new Check { CheckNumber = firstNumber });
            await _arrangeContext.SaveChangesAsync();

            var checks = await _controller.GetChecks();

            var firstCheck = checks.Value.First();
            Assert.Equal(firstNumber, firstCheck.CheckNumber);
        }
    }
}