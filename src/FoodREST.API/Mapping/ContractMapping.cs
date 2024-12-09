using Ardalis.Result;
using FoodREST.Application.Queries;
using FoodREST.API.Requests;
using FoodREST.API.Responses;
using FoodREST.Domain;

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

    public static FoodsResponse MapToResponse(this IEnumerable<Food> foods, int page, int pageSize, int totalCount)
    {
        return new FoodsResponse()
        {
            Items = foods.Select(MapToResponse),
            Page = page,
            PageSize = pageSize,
            Total = totalCount,
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

    public static GetAllFoodsOptions MapToOptions(this GetAllFoodsRequest request)
    {
        return new GetAllFoodsOptions()
        {
            Name = request.Name,
            SortField = request.SortBy?.Trim('+', '-'),
            SortOrder = request.SortBy is null
                ? SortOrder.Unsorted
                : request.SortBy.StartsWith('-')
                    ? SortOrder.Descending
                    : SortOrder.Ascending,
            Page = request.Page,
            PageSize = request.PageSize,
        };
    }
}
