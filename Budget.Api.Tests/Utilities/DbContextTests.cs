using System;
using Budget.Api.Tests.TestDoubles;

namespace Budget.Api.Tests.Utilities;

public class DbContextTests
{
    protected readonly DatabaseContext_TestDouble _arrangeContext;
    protected readonly DatabaseContext_TestDouble _actContext;
    protected readonly DatabaseContext_TestDouble _assertContext;

    protected DbContextTests()
    {
        var id = Guid.NewGuid().ToString();
        _arrangeContext = new DatabaseContext_TestDouble(id);
        _actContext = new DatabaseContext_TestDouble(id);
        _assertContext = new DatabaseContext_TestDouble(id);
    }
}