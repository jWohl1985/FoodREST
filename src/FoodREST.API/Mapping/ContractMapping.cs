using FoodREST.Contracts.Responses;
using FoodREST.Domain;
using Microsoft.AspNetCore.Http.Features;

namespace FoodREST.API.Mapping;

public static class ContractMapping
{
    public static FoodResponse MapToResponse(this Food food)
    {
        return new FoodResponse
        { 
            Id = food.Id,
            Name = food.Name,
            Calories = food.Calories,
            ProteinGrams = food.ProteinGrams,
            CarbohydrateGrams = food.CarbohydrateGrams,
            FatGrams = food.FatGrams,
        };
    }

    public static FoodsResponse MapToResponse(this IEnumerable<Food> foods)
    {
        return new FoodsResponse()
        {
            Items = foods.Select(MapToResponse)
        };
    }
}
