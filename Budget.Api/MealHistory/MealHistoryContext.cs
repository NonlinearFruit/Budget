using Budget.Shared;
using Budget.Shared.MealHistory;
using Microsoft.EntityFrameworkCore;

namespace Budget.Api.MealHistory;

public class MealHistoryContext : DbContext, IMealHistoryContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Server=127.0.0.1;Port=5432;Database=Budget;User Id=postgres;Password=postgres;", o => o.MigrationsHistoryTable("__EFMigrationsHistory", "MealHistory"));
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        var entityTypes = new []
        {
            typeof(Meal),
        };
        foreach(var type in entityTypes)
            builder
                .Entity(type)
                .Property<DateTime>(nameof(BaseEntity.Created))
                .HasDefaultValueSql("now()");
    }

    public DbSet<Meal> Meals { get; set; }

    Task<int> IMealHistoryContext.SaveChangesAsync()
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

    public void Migrate()
    {
        Database.Migrate();
    }
}