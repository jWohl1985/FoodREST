namespace FoodREST.API.Responses;

public class FoodResponse
{
    public required Guid Id { get; init; }

    public required string Name { get; init; }

    public required int Calories { get; init; }

    public required int ProteinGrams { get; init; }

    public required int CarbohydrateGrams { get; init; }

    public required int FatGrams { get; init; }
}
