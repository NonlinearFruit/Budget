using System;
using System.Diagnostics.CodeAnalysis;
using Budget.Api.Meal;
using Microsoft.EntityFrameworkCore;

namespace Budget.Api.Tests.TestDoubles.Meal;

[SuppressMessage("ReSharper", "InconsistentNaming")]
public class MealContext_TestDouble : MealContext
{
    private readonly string _id;

    public MealContext_TestDouble(string? id = null)
    {
        _id = id ?? Guid.NewGuid().ToString();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase(_id);
    }
}