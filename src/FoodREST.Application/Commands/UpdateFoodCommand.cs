using Ardalis.Result;
using FoodREST.Domain;
using MediatR;

namespace FoodREST.Application.Commands;

public class UpdateFoodCommand : IRequest<Result<Food>>
{
    public UpdateFoodCommand(Guid id, string name, int calories, int proteinGrams, int carbohydrateGrams, int fatGrams)
    {
        Id = id;
        Name = name;
        Calories = calories;
        ProteinGrams = proteinGrams;
        CarbohydrateGrams = carbohydrateGrams;
        FatGrams = fatGrams;
    }

    public Guid Id { get; init; }

    public string Name { get; init; }

    public int Calories { get; init; }

    public int ProteinGrams { get; init; }

    public int CarbohydrateGrams { get; init; }

    public int FatGrams { get; init; }
}
