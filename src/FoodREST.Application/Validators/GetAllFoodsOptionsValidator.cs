using FluentValidation;
using FoodREST.Application.Queries;

namespace FoodREST.Application.Validators;

public class GetAllFoodsOptionsValidator : AbstractValidator<GetAllFoodsOptions>
{
    public static readonly string[] AcceptableSortFields =
    {
        "name"
    };

    public GetAllFoodsOptionsValidator()
    {
        RuleFor(x => x.SortField)
            .Must(x => x is null || AcceptableSortFields.Contains(x, StringComparer.OrdinalIgnoreCase))
            .WithMessage("You can only sort by 'name'");

        RuleFor(x => x.Page)
            .GreaterThanOrEqualTo(1);

        RuleFor(x => x.PageSize)
            .InclusiveBetween(1, 25)
            .WithMessage("You can get between 1 and 25 foods per page");
    }
}
