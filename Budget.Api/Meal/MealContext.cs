using Budget.Api.Utilities;
using Microsoft.EntityFrameworkCore;

namespace Budget.Api.Meal;

public class MealContext : ContextBase, IMealContext
{
    public MealContext()
    {
        DatabaseSchema = "MealHistory";
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        ConfigureCreatedPropertyToDefault(builder, typeof(Shared.Meal.Meal));
    }

    public DbSet<Shared.Meal.Meal> Meals { get; set; }
}