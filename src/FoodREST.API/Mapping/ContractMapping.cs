using Ardalis.Result;
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

    public static ValidationFailureResponse MapToResponse(this IEnumerable<ValidationError> errors)
    {
        IEnumerable<ValidationResponse> responses = errors.Select(e => new ValidationResponse()
        {
            PropertyName = e.Identifier,
            Message = e.ErrorMessage,
        });

        return new ValidationFailureResponse()
        {
            Errors = responses,
        };
    }
}
