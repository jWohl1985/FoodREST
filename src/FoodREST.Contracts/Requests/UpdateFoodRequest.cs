﻿namespace FoodREST.Contracts.Requests;

public class UpdateFoodRequest
{
    public required string Name { get; init; }

    public required int Calories { get; init; }

    public required int ProteinGrams { get; init; }

    public required int CarbohydrateGrams { get; init; }

    public required int FatGrams { get; init; }
}
