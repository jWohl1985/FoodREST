using Ardalis.Result;
using FluentAssertions;
using FoodREST.Application.Interfaces;
using FoodREST.Application.Queries;
using FoodREST.Domain;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace FoodREST.Application.Tests.Unit.Queries;

public class GetFoodQueryHandlerTests
{
    private readonly Guid guid;
    private readonly IFoodRepository _foodRepository = Substitute.For<IFoodRepository>();

    private GetFoodQueryHandler _sut;

    public GetFoodQueryHandlerTests()
    {
        guid = Guid.NewGuid();
        _sut = new GetFoodQueryHandler(_foodRepository);
    }

    [Fact]
    public async Task Handle_ShouldReturnFood_WhenRequestIsValid()
    {
        // Arrange
        var query = new GetFoodQuery() { Id = guid };
        var banana = new Food(guid, "Banana", 110, 2, 27, 0);

        _foodRepository.GetByIdAsync(guid).Returns(banana);

        // Act
        var result = await _sut.Handle(query, default);

        // Assert
        await _foodRepository.Received(1).GetByIdAsync(guid);
        result.IsError().Should().BeFalse();
        result.Value.Should().BeEquivalentTo(banana, options => options.Excluding(o => o.Id));
    }

    [Fact]
    public async Task Handle_ShouldReturnNotFound_WhenDoesNotExist()
    {
        // Arrange
        var query = new GetFoodQuery() { Id = guid };
        _foodRepository.GetByIdAsync(guid).ReturnsNull();

        // Act
        var result = await _sut.Handle(query, default);

        // Assert
        await _foodRepository.Received(1).GetByIdAsync(guid);
        result.IsNotFound().Should().BeTrue();
    }
}
