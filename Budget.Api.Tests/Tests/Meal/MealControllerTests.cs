using Budget.Api.Meal;

namespace Budget.Api.Tests.Tests.MealHistory;

public class MealControllerTests
{
    private readonly MealController _controller;

    public MealControllerTests()
    {
        _controller = new MealController(default);
    }
}