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
        Name = name;
        Calories = calories;
        ProteinGrams = proteinGrams;
        CarbohydrateGrams = carbohydrateGrams;
        FatGrams = fatGrams;
    }

    public Guid Id { get; private set; }

    public string Name { get; private set; } = string.Empty;

    public int Calories { get; private set; }

    public int ProteinGrams { get; private set; }

    public int CarbohydrateGrams { get; private set; }

    public int FatGrams { get; private set; }
}
