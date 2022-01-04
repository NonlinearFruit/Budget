using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace Budget.Api.Tests.TestDoubles;

[SuppressMessage("ReSharper", "InconsistentNaming")]
public class DatabaseContext_TestDouble : DatabaseContext
{
    private readonly string _id;

    public DatabaseContext_TestDouble(string? id = null)
    {
        _id = id ?? Guid.NewGuid().ToString();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase(_id);
    }
}