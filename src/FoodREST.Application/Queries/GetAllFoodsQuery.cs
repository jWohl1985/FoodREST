using FoodREST.Domain;
using MediatR;

namespace FoodREST.Application.Queries;

public class GetAllFoodsQuery : IRequest<IEnumerable<Food>>
{

}
