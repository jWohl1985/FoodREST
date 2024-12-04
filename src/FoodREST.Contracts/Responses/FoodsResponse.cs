namespace FoodREST.Contracts.Responses;

public class FoodsResponse
{
    public IEnumerable<FoodResponse> Items { get; init; } = Enumerable.Empty<FoodResponse>();
}
