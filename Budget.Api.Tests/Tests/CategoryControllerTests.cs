using System.Linq;
using Budget.Api.Tests.Utilities;
using Budget.Shared;
using Xunit;

namespace Budget.Api.Tests.Tests;

public class CategoryControllerTests : BudgetContextTests
{
    private readonly CategoryController _controller;

    protected CategoryControllerTests()
    {
        _controller = new CategoryController(_actContext);
    }

    public class GetCategories : CategoryControllerTests
    {
        [Fact]
        public void categories_are_alphabetized()
        {
            var first = "A";
            var second = "B";
            _assertContext.Categories.Add(new Category
            {
                Name = second, Color = ""
            });
            _assertContext.Categories.Add(new Category
            {
                Name = first, Color = ""
            });
            _assertContext.SaveChanges();

            var categories = _controller.GetCategories().Result.Value;

            var category = categories.First();
            Assert.Equal(first, category.Name);
        }
    }
}