using FoodREST.Domain;
using FoodREST.Infrastructure.Persistence.Configuration;
using Microsoft.EntityFrameworkCore;

namespace FoodREST.Infrastructure.Persistence;

public class FoodContext : DbContext
{
    public FoodContext(DbContextOptions<FoodContext> options) : base(options)
    {

    }

    public DbSet<Food> Foods => Set<Food>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new FoodConfiguration());
    }
}
