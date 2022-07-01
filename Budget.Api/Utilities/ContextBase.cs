using Budget.Shared;
using Microsoft.EntityFrameworkCore;

namespace Budget.Api.Utilities;

public abstract class ContextBase : DbContext
{
    protected string DatabaseSchema { get; set; } = "public";

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Server=127.0.0.1;Port=5432;Database=Budget;User Id=postgres;Password=postgres;", o => o.MigrationsHistoryTable("__EFMigrationsHistory", DatabaseSchema));
    }

    protected void ConfigureCreatedPropertyToDefault(ModelBuilder builder, params Type[] entityTypes)
    {
        foreach(var type in entityTypes)
            builder
                .Entity(type)
                .Property<DateTime>(nameof(BaseEntity.Created))
                .HasDefaultValueSql("now()");
    }

    public Task<int> SaveChangesAsync()
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