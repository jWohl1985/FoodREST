namespace FoodREST.API.Requests;

public class CreateFoodRequest
{
    public required string Name { get; init; }

    public required int Calories { get; init; }

    public required int ProteinGrams { get; init; }

    public required int CarbohydrateGrams { get; init; }

    public required int FatGrams { get; init; }
}
