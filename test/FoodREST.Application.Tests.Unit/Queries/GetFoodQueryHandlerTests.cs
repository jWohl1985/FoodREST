using Ardalis.Result;
using FluentAssertions;
using FoodREST.Application.Interfaces;
using FoodREST.Application.Queries;
using FoodREST.Domain;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace FoodREST.Application.Tests.Unit.Queries;

public class GetFoodQueryHandlerTests : IClassFixture<FoodFixture>
{
    private readonly IFoodRepository _foodRepository = Substitute.For<IFoodRepository>();

    private Food _banana;
    private GetFoodQueryHandler _sut;

    public GetFoodQueryHandlerTests(FoodFixture foodFixture)
    {
        _banana = foodFixture.Banana;
        _sut = new GetFoodQueryHandler(_foodRepository);
    }

    [Fact]
    public async Task Handle_ShouldReturnFood_WhenRequestIsValid()
    {
        // Arrange
        var query = new GetFoodQuery() { Id = _banana.Id };

        _foodRepository.GetByIdAsync(_banana.Id).Returns(_banana);

        // Act
        var result = await _sut.Handle(query, default);

        // Assert
        await _foodRepository.Received(1).GetByIdAsync(_banana.Id);
        result.IsError().Should().BeFalse();
        result.Value.Should().BeEquivalentTo(_banana, options => options.Excluding(o => o.Id));
    }

    [Fact]
    public async Task Handle_ShouldReturnNotFound_WhenDoesNotExist()
    {
        // Arrange
        var query = new GetFoodQuery() { Id = _banana.Id };
        _foodRepository.GetByIdAsync(_banana.Id).ReturnsNull();

        // Act
        var result = await _sut.Handle(query, default);

        // Assert
        await _foodRepository.Received(1).GetByIdAsync(_banana.Id);
        result.IsNotFound().Should().BeTrue();
    }
}
