using FoodREST.Application.Interfaces;
using FoodREST.Domain;
using MediatR;

namespace FoodREST.Application.Queries;

public class GetAllFoodsQueryHandler : IRequestHandler<GetAllFoodsQuery, IEnumerable<Food>>
{
    private readonly IFoodRepository _foodRepository;

    public GetAllFoodsQueryHandler(IFoodRepository foodRepository)
    {
        _foodRepository = foodRepository;
    }

    public async Task<IEnumerable<Food>> Handle(GetAllFoodsQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<Food> foods = await _foodRepository.GetAllAsync();

        return foods;
    }
}
