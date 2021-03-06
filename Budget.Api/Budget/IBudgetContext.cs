using Budget.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Budget.Api.Budget;

public interface IBudgetContext
{
    DbSet<BankAccount> BankAccounts { get; set; }
    DbSet<Tag> Tags { get; set; }
    DbSet<Transaction> Transactions { get; set; }
    DbSet<Forecast> Forecasts { get; set; }
    DbSet<Category> Categories { get; set; }
    DbSet<Check> Checks { get; set; }
    EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
    Task<int> SaveChangesAsync();
    void Migrate();
}