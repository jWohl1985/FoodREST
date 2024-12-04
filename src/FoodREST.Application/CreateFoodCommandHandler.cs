using Ardalis.Result;
using FoodREST.Domain;
using MediatR;

namespace FoodREST.Application;

public class CreateFoodCommandHandler : IRequestHandler<CreateFoodCommand, Result>
{
    private readonly IFoodRepository _foodRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateFoodCommandHandler(IFoodRepository foodRepository, IUnitOfWork unitOfWork)
    {
        _foodRepository = foodRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(CreateFoodCommand request, CancellationToken cancellationToken)
    {
        Food food = new(
            name: request.Name,
            calories: request.Calories,
            proteinGrams: request.ProteinGrams,
            carbohydrateGrams: request.CarbohydrateGrams,
            fatGrams: request.FatGrams);

        bool success = await _foodRepository.AddFoodAsync(food);

        if (!success)
        {
            return Result.Error();
        }

        await _unitOfWork.SaveChangesAsync();

        return Result.Success();
    }
}
