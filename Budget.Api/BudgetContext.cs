using Budget.Api.Utilities;
using Budget.Shared;
using Microsoft.EntityFrameworkCore;

namespace Budget.Api;

public class BudgetContext : ContextBase, IBudgetContext
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        ConfigureCreatedPropertyToDefault(builder,
            typeof(BankAccount),
            typeof(Category),
            typeof(Check),
            typeof(Forecast),
            typeof(Tag),
            typeof(Transaction)
        );
    }

    public DbSet<BankAccount> BankAccounts { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Check> Checks { get; set; }
    public DbSet<Forecast> Forecasts { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
}