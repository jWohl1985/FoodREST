using Ardalis.Result;
using FluentValidation;
using FoodREST.Application.Interfaces;
using FoodREST.Domain;
using MediatR;

namespace FoodREST.Application.Commands;

public class CreateFoodCommandHandler : IRequestHandler<CreateFoodCommand, Result<Food>>
{
    private readonly IFoodRepository _foodRepository;
    private readonly IValidator<CreateFoodCommand> _validator;

    public CreateFoodCommandHandler(IFoodRepository foodRepository,
        IValidator<CreateFoodCommand> validator)
    {
        _foodRepository = foodRepository;
        _validator = validator;
    }

    public async Task<Result<Food>> Handle(CreateFoodCommand request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        Food food = new(
            name: request.Name,
            calories: request.Calories,
            proteinGrams: request.ProteinGrams,
            carbohydrateGrams: request.CarbohydrateGrams,
            fatGrams: request.FatGrams,
            id: Guid.NewGuid());

        bool success = await _foodRepository.AddFoodAsync(food, cancellationToken);

        if (!success)
        {
            return Result.Error();
        }

        return Result.Success(food);
    }
}
