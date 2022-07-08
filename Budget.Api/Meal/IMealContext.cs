using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Budget.Api.Meal;

public interface IMealContext
{
    public DbSet<Shared.Meal.Meal> Meals { get; set; }
    EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
    Task<int> SaveChangesAsync();
    void Migrate();
}