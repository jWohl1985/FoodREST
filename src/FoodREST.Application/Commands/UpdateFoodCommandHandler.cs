using Ardalis.Result;
using FoodREST.Application.Interfaces;
using FoodREST.Contracts.Responses;
using FoodREST.Domain;
using MediatR;

namespace FoodREST.Application.Commands;

public class UpdateFoodCommandHandler : IRequestHandler<UpdateFoodCommand, Result<Food>>
{
    private readonly IFoodRepository _foodRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateFoodCommandHandler(IFoodRepository foodRepository, IUnitOfWork unitOfWork)
    {
        _foodRepository = foodRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Food>> Handle(UpdateFoodCommand request, CancellationToken cancellationToken)
    {
        Food newFood = new(
            name: request.Name, 
            calories: request.Calories, 
            proteinGrams: request.ProteinGrams, 
            carbohydrateGrams: request.CarbohydrateGrams, 
            fatGrams: request.FatGrams,
            id: request.Id);

        var result = await _foodRepository.UpdateFoodAsync(request.Id, newFood);

        if (result is null)
        {
            return Result.NotFound();
        }

        await _unitOfWork.SaveChangesAsync();
        return Result.Success(result);
    }
}
