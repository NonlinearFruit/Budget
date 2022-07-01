using Budget.Api.Utilities;
using Budget.Shared.MealHistory;
using Microsoft.EntityFrameworkCore;

namespace Budget.Api.MealHistory;

public class MealHistoryContext : ContextBase, IMealHistoryContext
{
    public MealHistoryContext()
    {
        DatabaseSchema = "MealHistory";
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        ConfigureCreatedPropertyToDefault(builder, typeof(Meal));
    }

    public DbSet<Meal> Meals { get; set; }
}