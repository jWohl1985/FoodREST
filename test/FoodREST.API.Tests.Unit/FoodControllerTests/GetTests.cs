using Ardalis.Result;
using FluentAssertions;
using FoodREST.API.Controllers;
using FoodREST.Application.Queries;
using FoodREST.Contracts.Responses;
using FoodREST.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;

namespace FoodREST.API.Tests.Unit.FoodControllerTests;

public class GetTests
{
    private readonly IMediator _mediator = Substitute.For<IMediator>();

    private readonly FoodController _sut;

    public GetTests()
    {
        _sut = new FoodController(_mediator);
    }

    [Fact]
    public async Task Get_ShouldReturnOkAndObject_WhenFoodExists()
    {
        // Arrange
        GetFoodQuery query = new GetFoodQuery() { Id = Guid.NewGuid() };
        Food food = new(query.Id, "Banana", 110, 2, 27, 1);
        _mediator.Send(Arg.Is<GetFoodQuery>(q => q.Id == query.Id)).Returns(Result.Success(food));

        var expectedResponse = new FoodResponse()
        {
            Id = food.Id,
            Name = food.Name,
            Calories = food.Calories,
            ProteinGrams = food.ProteinGrams,
            CarbohydrateGrams = food.CarbohydrateGrams,
            FatGrams = food.FatGrams,
        };

        // Act
        var result = (OkObjectResult)await _sut.Get(query.Id, default);

        // Assert
        await _mediator.Received(1).Send(Arg.Is<GetFoodQuery>(q => q.Id == query.Id));
        result.StatusCode.Should().Be(200);
        result.Value.Should().BeEquivalentTo(expectedResponse);
    }

    [Fact]
    public async Task Get_ShouldReturnNotFound_WhenFoodDoesNotExist()
    {
        // Arrange
        GetFoodQuery query = new GetFoodQuery() { Id = Guid.NewGuid() };
        _mediator.Send(Arg.Is<GetFoodQuery>(q => q.Id == query.Id)).Returns(Result.NotFound());

        // Act
        var result = (NotFoundResult)await _sut.Get(query.Id, default);

        // Assert
        await _mediator.Received(1).Send(Arg.Is<GetFoodQuery>(q => q.Id == query.Id));
        result.StatusCode.Should().Be(404);
    }
}
