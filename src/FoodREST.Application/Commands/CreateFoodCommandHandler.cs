using Ardalis.Result;
using FoodREST.Application.Interfaces;
using FoodREST.Contracts.Responses;
using FoodREST.Domain;
using MediatR;

namespace FoodREST.Application.Commands;

public class CreateFoodCommandHandler : IRequestHandler<CreateFoodCommand, Result<Food>>
{
    private readonly IFoodRepository _foodRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateFoodCommandHandler(IFoodRepository foodRepository, IUnitOfWork unitOfWork)
    {
        _foodRepository = foodRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Food>> Handle(CreateFoodCommand request, CancellationToken cancellationToken)
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

        return food;
    }
}
