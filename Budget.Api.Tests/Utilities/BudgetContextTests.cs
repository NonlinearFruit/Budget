using System;
using Budget.Api.Tests.TestDoubles;
using Budget.Api.Tests.TestDoubles.Budget;

namespace Budget.Api.Tests.Utilities;

public class BudgetContextTests
{
    protected readonly BudgetContext_TestDouble _arrangeContext;
    protected readonly BudgetContext_TestDouble _actContext;
    protected readonly BudgetContext_TestDouble _assertContext;

    protected BudgetContextTests()
    {
        var id = Guid.NewGuid().ToString();
        _arrangeContext = new BudgetContext_TestDouble(id);
        _actContext = new BudgetContext_TestDouble(id);
        _assertContext = new BudgetContext_TestDouble(id);
    }
}