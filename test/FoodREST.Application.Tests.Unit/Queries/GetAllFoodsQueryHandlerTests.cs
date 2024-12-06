using FluentAssertions;
using FoodREST.Application.Interfaces;
using FoodREST.Application.Queries;
using FoodREST.Domain;
using NSubstitute;

namespace FoodREST.Application.Tests.Unit.Queries;

public class GetAllFoodsQueryHandlerTests : IClassFixture<FoodFixture>
{
    private readonly IFoodRepository _foodRepository = Substitute.For<IFoodRepository>();

    private GetAllFoodsQueryHandler _sut;
    private Food _banana;
    private Food _beefJerky;

    public GetAllFoodsQueryHandlerTests(FoodFixture foodFixture)
    {
        _banana = foodFixture.Banana;
        _beefJerky = foodFixture.BeefJerky;
        _sut = new GetAllFoodsQueryHandler(_foodRepository);
    }

    [Fact]
    public async Task Handle_ShouldReturnFoods_WhenTheyExist()
    {
        // Arrange
        var query = new GetAllFoodsQuery();
        List<Food> foods = [_banana, _beefJerky];
        _foodRepository.GetAllAsync().Returns(foods);

        // Act
        var result = await _sut.Handle(query, default);

        // Assert
        await _foodRepository.Received(1).GetAllAsync();
        result.Should().BeEquivalentTo(foods);
    }

    [Fact]
    public async Task Handle_ShouldReturnEmptyEnumerable_WhenNoFoodsExist()
    {
        // Arrange
        var query = new GetAllFoodsQuery();
        List<Food> foods = new();
        _foodRepository.GetAllAsync().Returns(foods);

        // Act
        var result = await _sut.Handle(query, default);

        // Assert
        await _foodRepository.Received(1).GetAllAsync();
        result.Should().BeEquivalentTo(Enumerable.Empty<Food>());
    }
}
