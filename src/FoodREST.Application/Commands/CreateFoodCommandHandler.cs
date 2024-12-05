using Ardalis.Result;
using FluentValidation;
using FluentValidation.Results;
using FoodREST.Application.Interfaces;
using FoodREST.Domain;
using MediatR;

namespace FoodREST.Application.Commands;

public class CreateFoodCommandHandler : IRequestHandler<CreateFoodCommand, Result<Food>>
{
    private readonly IFoodRepository _foodRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<CreateFoodCommand> _validator;

    public CreateFoodCommandHandler(IFoodRepository foodRepository, 
        IUnitOfWork unitOfWork, 
        IValidator<CreateFoodCommand> validator)
    {
        _foodRepository = foodRepository;
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    public async Task<Result<Food>> Handle(CreateFoodCommand request, CancellationToken cancellationToken)
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

        return Result.Success(food);
    }
}
