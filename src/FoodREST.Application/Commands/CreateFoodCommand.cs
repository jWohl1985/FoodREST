using Ardalis.Result;
using FoodREST.Domain;
using MediatR;

namespace FoodREST.Application.Commands;

public sealed class CreateFoodCommand : IRequest<Result<Food>>
{
    public CreateFoodCommand(string name, int calories, int proteinGrams, int carbohydrateGrams, int fatGrams)
    {
        Name = name;
        Calories = calories;
        ProteinGrams = proteinGrams;
        CarbohydrateGrams = carbohydrateGrams;
        FatGrams = fatGrams;
    }

    public string Name { get; init; }

    public int Calories { get; init; }

    public int ProteinGrams { get; init; }

    public int CarbohydrateGrams { get; init; }

    public int FatGrams { get; init; }
}
