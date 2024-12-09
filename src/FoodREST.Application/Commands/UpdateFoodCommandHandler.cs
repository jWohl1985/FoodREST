using Ardalis.Result;
using FluentValidation;
using FluentValidation.Results;
using FoodREST.Application.Interfaces;
using FoodREST.Domain;
using MediatR;

namespace FoodREST.Application.Commands;

public sealed class UpdateFoodCommandHandler : IRequestHandler<UpdateFoodCommand, Result<Food>>
{
    private readonly IFoodRepository _foodRepository;
    private readonly IValidator<UpdateFoodCommand> _validator;

    public UpdateFoodCommandHandler(IFoodRepository foodRepository,
        IValidator<UpdateFoodCommand> validator)
    {
        _foodRepository = foodRepository;
        _validator = validator;
    }

    public async Task<Result<Food>> Handle(UpdateFoodCommand request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        Food newFood = new(
            name: request.Name, 
            calories: request.Calories, 
            proteinGrams: request.ProteinGrams, 
            carbohydrateGrams: request.CarbohydrateGrams, 
            fatGrams: request.FatGrams,
            id: request.Id);

        var result = await _foodRepository.UpdateFoodAsync(request.Id, newFood, cancellationToken);

        if (result is null)
        {
            return Result.NotFound();
        }

        return Result.Success(result);
    }
}
