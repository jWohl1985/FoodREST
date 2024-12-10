using FoodREST.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodREST.Infrastructure.Persistence.Configuration;

public class FoodConfiguration : IEntityTypeConfiguration<Food>
{
    public void Configure(EntityTypeBuilder<Food> builder)
    {
        builder.HasKey(f => f.Id);

        builder.Property(f => f.Id)
            .ValueGeneratedNever();

        builder.Property(f => f.Name)
            .HasMaxLength(150);

        builder.Property(f => f.Calories);

        builder.Property(f => f.ProteinGrams);

        builder.Property(f => f.CarbohydrateGrams);

        builder.Property(f => f.FatGrams);
    }
}
