using Ardalis.Result;
using FoodREST.Application.Interfaces;
using FoodREST.Domain;
using MediatR;

namespace FoodREST.Application.Queries;

public sealed class GetFoodQueryHandler : IRequestHandler<GetFoodQuery, Result<Food>>
{
    private readonly IFoodRepository _foodRepository;

    public GetFoodQueryHandler(IFoodRepository foodRepository)
    {
        _foodRepository = foodRepository;
    }

    public async Task<Result<Food>> Handle(GetFoodQuery request, CancellationToken cancellationToken)
    {
        Food? food = await _foodRepository.GetByIdAsync(request.Id, cancellationToken);

        if (food is null)
        {
            return Result.NotFound();
        }

        return Result.Success(food);
    }
}
