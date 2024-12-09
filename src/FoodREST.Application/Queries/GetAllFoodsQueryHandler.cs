using FluentValidation;
using FoodREST.Application.Interfaces;
using FoodREST.Domain;
using MediatR;

namespace FoodREST.Application.Queries;

public sealed class GetAllFoodsQueryHandler : IRequestHandler<GetAllFoodsQuery, (IEnumerable<Food> Foods, int Count)>
{
    private readonly IFoodRepository _foodRepository;
    private readonly IValidator<GetAllFoodsOptions> _optionsValidator;

    public GetAllFoodsQueryHandler(IFoodRepository foodRepository, IValidator<GetAllFoodsOptions> optionsValidator)
    {
        _foodRepository = foodRepository;
        _optionsValidator = optionsValidator;
    }

    public async Task<(IEnumerable<Food> Foods, int Count)> Handle(GetAllFoodsQuery request, CancellationToken cancellationToken)
    {
        await _optionsValidator.ValidateAndThrowAsync(request.Options, cancellationToken);

        IEnumerable<Food> foods = await _foodRepository.GetAllAsync(request.Options, cancellationToken);
        int count = await _foodRepository.GetCountAsync(request.Options, cancellationToken);

        return (foods, count);
    }
}
