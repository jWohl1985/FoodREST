using Ardalis.Result;
using FluentValidation;
using FluentValidation.Results;
using FoodREST.Application.Interfaces;
using FoodREST.Domain;
using MediatR;

namespace FoodREST.Application.Commands;

public class UpdateFoodCommandHandler : IRequestHandler<UpdateFoodCommand, Result<Food>>
{
    private readonly IFoodRepository _foodRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<UpdateFoodCommand> _validator;

    public UpdateFoodCommandHandler(IFoodRepository foodRepository, 
        IUnitOfWork unitOfWork,
        IValidator<UpdateFoodCommand> validator)
    {
        _foodRepository = foodRepository;
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    public async Task<Result<Food>> Handle(UpdateFoodCommand request, CancellationToken cancellationToken)
    {
        ValidationResult validation = await _validator.ValidateAsync(request, cancellationToken);

        if (!validation.IsValid)
        {
            List<ValidationError> validationProblems = new();

            foreach (var error in validation.Errors)
            {
                ValidationError problem = new(error.PropertyName, error.ErrorMessage);
                validationProblems.Add(problem);
            }

            return Result.Invalid(validationProblems);
        }

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

        await _unitOfWork.SaveChangesAsync();
        return Result.Success(result);
    }
}
