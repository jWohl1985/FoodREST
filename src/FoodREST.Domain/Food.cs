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

    public string Name { get; set; } = string.Empty;

    public int Calories { get; set; }

    public int ProteinGrams { get; set; }

    public int CarbohydrateGrams { get; set; }

    public int FatGrams { get; set; }

    private Food()
    {

    }
}
