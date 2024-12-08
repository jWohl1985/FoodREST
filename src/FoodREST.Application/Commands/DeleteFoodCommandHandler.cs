using Ardalis.Result;
using FoodREST.Application.Interfaces;
using MediatR;

namespace FoodREST.Application.Commands;

public class DeleteFoodCommandHandler : IRequestHandler<DeleteFoodCommand, Result>
{
    private readonly IFoodRepository _foodRepository;

    public DeleteFoodCommandHandler(IFoodRepository foodRepository)
    {
        _foodRepository = foodRepository;
    }

    public async Task<Result> Handle(DeleteFoodCommand request, CancellationToken cancellationToken)
    {
        bool success = await _foodRepository.DeleteFoodAsync(request.Id, cancellationToken);

        if (!success)
        {
            return Result.NotFound();
        }

        return Result.Success();
    }
}
