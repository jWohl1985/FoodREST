using FluentAssertions;
using FluentValidation;
using FoodREST.Application.Interfaces;
using FoodREST.Application.Queries;
using FoodREST.Domain;
using NSubstitute;

namespace FoodREST.Application.Tests.Unit.Queries;

public class GetAllFoodsQueryHandlerTests : IClassFixture<FoodFixture>
{
    private readonly IFoodRepository _foodRepository = Substitute.For<IFoodRepository>();
    private readonly IValidator<GetAllFoodsOptions> _optionsValidator = Substitute.For<IValidator<GetAllFoodsOptions>>();

    private GetAllFoodsQueryHandler _sut;
    private Food _banana;
    private Food _beefJerky;

    public GetAllFoodsQueryHandlerTests(FoodFixture foodFixture)
    {
        _banana = foodFixture.Banana;
        _beefJerky = foodFixture.BeefJerky;
        _sut = new GetAllFoodsQueryHandler(_foodRepository, _optionsValidator);
    }

    [Fact]
    public async Task Handle_ShouldReturnFoods_WhenTheyExist()
    {
        // Arrange
        var query = new GetAllFoodsQuery() { Options = new GetAllFoodsOptions() { } };
        List<Food> foods = [_banana, _beefJerky];
        _foodRepository.GetAllAsync(query.Options).Returns(foods);
        _foodRepository.GetCountAsync(query.Options).Returns(2);

        // Act
        var result = await _sut.Handle(query, default);

        // Assert
        await _foodRepository.Received(1).GetAllAsync(query.Options);
        await _foodRepository.Received(1).GetCountAsync(query.Options);
        result.Foods.Should().BeEquivalentTo(foods);
        result.Count.Should().Be(2);
    }

    [Fact]
    public async Task Handle_ShouldReturnEmptyEnumerable_WhenNoFoodsExist()
    {
        // Arrange
        var query = new GetAllFoodsQuery() { Options = new GetAllFoodsOptions() };
        List<Food> foods = new();
        _foodRepository.GetAllAsync(query.Options).Returns(foods);
        _foodRepository.GetCountAsync(query.Options).Returns(0);

        // Act
        var result = await _sut.Handle(query, default);

        // Assert
        await _foodRepository.Received(1).GetAllAsync(query.Options);
        await _foodRepository.Received(1).GetCountAsync(query.Options);
        result.Foods.Should().BeEquivalentTo(Enumerable.Empty<Food>());
        result.Count.Should().Be(0);
    }
}
