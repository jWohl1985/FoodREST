using FluentValidation;
using FoodREST.Application.Commands;

namespace FoodREST.Application.Validators;

public sealed class CreateFoodCommandValidator : AbstractValidator<CreateFoodCommand>
{
    public CreateFoodCommandValidator()
    {
        RuleFor(food => food.Name)
            .NotEmpty()
            .MaximumLength(150);

        RuleFor(food => food.Calories)
            .GreaterThanOrEqualTo(0);

        RuleFor(food => food.ProteinGrams)
            .GreaterThanOrEqualTo(0);

        RuleFor(food => food.CarbohydrateGrams)
            .GreaterThanOrEqualTo(0);

        RuleFor(food => food.FatGrams)
            .GreaterThanOrEqualTo(0);
    }
}
