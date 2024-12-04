using Ardalis.GuardClauses;
using Ardalis.Result;
using FoodREST.Domain;
using MediatR;

namespace FoodREST.Application.Commands;

public class CreateFoodCommand : IRequest<Result<Food>>
{
    public CreateFoodCommand(string name, int calories, int proteinGrams, int carbohydrateGrams, int fatGrams)
    {
        Name = Guard.Against.NullOrEmpty(name);
        Calories = Guard.Against.Negative(calories);
        ProteinGrams = Guard.Against.Negative(proteinGrams);
        CarbohydrateGrams = Guard.Against.Negative(carbohydrateGrams);
        FatGrams = Guard.Against.Negative(fatGrams);
    }

    public string Name { get; init; }

    public int Calories { get; init; }

    public int ProteinGrams { get; init; }

    public int CarbohydrateGrams { get; init; }

    public int FatGrams { get; init; }
}
