using Ardalis.Result;
using FoodREST.Application.Interfaces;
using MediatR;

namespace FoodREST.Application.Commands;

public class DeleteFoodCommandHandler : IRequestHandler<DeleteFoodCommand, Result>
{
    private readonly IFoodRepository _foodRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteFoodCommandHandler(IFoodRepository foodRepository, IUnitOfWork unitOfWork)
    {
        _foodRepository = foodRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(DeleteFoodCommand request, CancellationToken cancellationToken)
    {
        bool success = await _foodRepository.DeleteFoodAsync(request.Id, cancellationToken);

        if (!success)
        {
            return Result.NotFound();
        }

        await _unitOfWork.SaveChangesAsync();
        return Result.Success();
    }
}
