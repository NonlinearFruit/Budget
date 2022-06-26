using Budget.Shared;
using Budget.Shared.MealHistory;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Budget.Api.MealHistory;

public interface IMealHistoryContext
{
    public DbSet<Meal> Meals { get; set; }
    EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
    Task<int> SaveChangesAsync();
    void Migrate();
}