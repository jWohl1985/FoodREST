using Ardalis.GuardClauses;

namespace FoodREST.Domain;

public sealed class Food
{
    public Food(
        Guid? id,
        string name, 
        int calories, 
        int proteinGrams, 
        int carbohydrateGrams, 
        int fatGrams)
    {
        Id = id ?? Guid.NewGuid();
        Name = Guard.Against.NullOrEmpty(name);
        Calories = Guard.Against.Negative(calories);
        ProteinGrams = Guard.Against.Negative(proteinGrams);
        CarbohydrateGrams = Guard.Against.Negative(carbohydrateGrams);
        FatGrams = Guard.Against.Negative(fatGrams);
    }

    public Guid Id { get; private set; }

    public string Name { get; private set; } = string.Empty;

    public int Calories { get; private set; }

    public int ProteinGrams { get; private set; }

    public int CarbohydrateGrams { get; private set; }

    public int FatGrams { get; private set; }
}
