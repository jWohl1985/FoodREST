﻿using FluentAssertions;
using FoodREST.Application.Interfaces;
using FoodREST.Application.Queries;
using FoodREST.Domain;
using NSubstitute;

namespace FoodREST.Application.Tests.Unit;

public class GetAllFoodsQueryHandlerTests
{
    private readonly IFoodRepository _foodRepository = Substitute.For<IFoodRepository>();

    private GetAllFoodsQueryHandler _sut;

    public GetAllFoodsQueryHandlerTests()
    {
        _sut = new GetAllFoodsQueryHandler(_foodRepository);
    }

    [Fact]
    public async Task Handle_ShouldReturnFoods_WhenTheyExist()
    {
        // Arrange
        var query = new GetAllFoodsQuery();
        var banana = new Food("Banana", 110, 2, 27, 0);
        List<Food> foods = [banana];
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
