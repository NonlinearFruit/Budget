using Budget.Shared;
using Microsoft.EntityFrameworkCore;

namespace Budget.Api;

public class DatabaseContext : DbContext, IDatabaseContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Server=127.0.0.1;Port=5432;Database=Budget;User Id=postgres;Password=postgres;");
        optionsBuilder.EnableSensitiveDataLogging();
        optionsBuilder.EnableDetailedErrors();
        optionsBuilder.LogTo(Console.WriteLine);
    }

    public DbSet<BankAccount> BankAccounts { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<Forecast> Forecasts { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Check> Checks { get; set; }

    public void Migrate()
    {
        Database.Migrate();
    }
}