namespace FoodREST.Application.Queries;

public class GetAllFoodsOptions
{
    public string? Name { get; set; }

    public string? SortField { get; set; }

    public SortOrder? SortOrder { get; set; }

    public int Page { get; set; }

    public int PageSize { get; set; }
}

public enum SortOrder
{
    Unsorted,
    Ascending,
    Descending,
}
