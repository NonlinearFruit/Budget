using System.Linq;
using Budget.Api.Tests.Utilities;
using Budget.Shared;
using Xunit;

namespace Budget.Api.Tests.Tests;

public class TagControllerTests : BudgetContextTests
{
    private readonly TagController _controller;

    protected TagControllerTests()
    {
        _controller = new TagController(_actContext);
    }

    public class GetTags : TagControllerTests
    {
        [Fact]
        public void tags_are_alphabetized()
        {
            var first = "A";
            var second = "B";
            _assertContext.Tags.Add(new Tag
            {
                Name = second, Color = ""
            });
            _assertContext.Tags.Add(new Tag
            {
                Name = first, Color = ""
            });
            _assertContext.SaveChanges();

            var tags = _controller.GetTags().Result.Value;

            var tag = tags.First();
            Assert.Equal(first, tag.Name);
        }
    }
}