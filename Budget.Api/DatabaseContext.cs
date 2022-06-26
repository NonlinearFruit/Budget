using Budget.Shared;
using Microsoft.EntityFrameworkCore;

namespace Budget.Api;

public class DatabaseContext : DbContext, IDatabaseContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Server=127.0.0.1;Port=5432;Database=Budget;User Id=postgres;Password=postgres;");
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        var entityTypes = new []
        {
            typeof(BankAccount),
            typeof(Category),
            typeof(Check),
            typeof(Forecast),
            typeof(Tag),
            typeof(Transaction)
        };
        foreach(var type in entityTypes)
            builder
                .Entity(type)
                .Property<DateTime>(nameof(BaseEntity.Created))
                .HasDefaultValueSql("now()");
    }

    Task<int> IDatabaseContext.SaveChangesAsync()
    {
        var now = new DateTime(DateTime.Now.Ticks, DateTimeKind.Utc);

        foreach (var entry in ChangeTracker.Entries())
        {
            if (entry.Entity is BaseEntity entity)
                switch (entry.State)
                {
                    case EntityState.Added:
                    case EntityState.Modified:
                        entity.Modified = now;
                        break;
                }
        }

        return base.SaveChangesAsync();
    }

    public DbSet<BankAccount> BankAccounts { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Check> Checks { get; set; }
    public DbSet<Forecast> Forecasts { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<Transaction> Transactions { get; set; }

    public void Migrate()
    {
        Database.Migrate();
    }
}