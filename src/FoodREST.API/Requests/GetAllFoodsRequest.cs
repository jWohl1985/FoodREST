namespace FoodREST.API.Requests;

public class GetAllFoodsRequest : PagedRequest
{
    public required string? Name { get; init; }

    public required string? SortBy { get; init; }
}
