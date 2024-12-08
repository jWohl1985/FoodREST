using FoodREST.Domain;
using MediatR;

namespace FoodREST.Application.Queries;

public class GetAllFoodsQuery : IRequest<(IEnumerable<Food> Foods, int Count)>
{
    public required GetAllFoodsOptions Options { get; init; }
}
