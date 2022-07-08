using System;
using Budget.Api.Tests.TestDoubles.Meal;

namespace Budget.Api.Tests.Utilities;

public class MealContextTests
{
    protected readonly MealContext_TestDouble _arrangeContext;
    protected readonly MealContext_TestDouble _actContext;
    protected readonly MealContext_TestDouble _assertContext;

    protected MealContextTests()
    {
        var id = Guid.NewGuid().ToString();
        _arrangeContext = new MealContext_TestDouble(id);
        _actContext = new MealContext_TestDouble(id);
        _assertContext = new MealContext_TestDouble(id);
    }
}