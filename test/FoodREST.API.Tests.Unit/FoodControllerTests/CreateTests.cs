using Ardalis.Result;
using FluentAssertions;
using FoodREST.API.Controllers;
using FoodREST.Application.Commands;
using FoodREST.Contracts.Requests;
using FoodREST.Contracts.Responses;
using FoodREST.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;

namespace FoodREST.API.Tests.Unit.FoodControllerTests;

public class CreateTests
{
    private readonly IMediator _mediator = Substitute.For<IMediator>();

    private readonly CreateFoodRequest _validRequest;
    private readonly CreateFoodRequest _invalidRequest;

    private readonly FoodController _sut;

    public CreateTests()
    {
        _validRequest = new CreateFoodRequest()
        {
            Name = "Banana",
            Calories = 110,
            ProteinGrams = 2,
            CarbohydrateGrams = 27,
            FatGrams = 1,
        };

        _invalidRequest = new CreateFoodRequest()
        {
            Name = string.Empty,
            Calories = -110,
            ProteinGrams = -5,
            CarbohydrateGrams = -1,
            FatGrams = -2,
        };

        _sut = new FoodController(_mediator);
    }

    [Fact]
    public async Task Create_ShouldReturnOkAndObject_WhenRequestIsValid()
    {
        // Arrange
        FoodResponse expectedResponse = new()
        {
            Id = Guid.NewGuid(),
            Name = "Banana",
            Calories = 110,
            ProteinGrams = 2,
            CarbohydrateGrams = 27,
            FatGrams = 1,
        };

        Food expectedFood = new(expectedResponse.Id,
            _validRequest.Name,
            _validRequest.Calories,
            _validRequest.ProteinGrams,
            _validRequest.CarbohydrateGrams,
            _validRequest.FatGrams);

        _mediator.Send(Arg.Is<CreateFoodCommand>(c => c.Name == _validRequest.Name)).Returns(Result.Success(expectedFood));

        // Act
        var result = (OkObjectResult)await _sut.Create(_validRequest, default);

        // Assert
        await _mediator.Received(1).Send(Arg.Is<CreateFoodCommand>(c => c.Name == _validRequest.Name));
        result.StatusCode.Should().Be(200);
        result.Value.Should().BeEquivalentTo(expectedResponse);
    }

    [Fact]
    public async Task Create_ShouldReturnBadRequestAndObject_WhenRequestIsInvalid()
    {
        // Arrange
        ValidationFailureResponse expectedResponse = new()
        {
            Errors = new List<ValidationResponse>()
            {
                new ValidationResponse() { PropertyName = nameof(CreateFoodRequest.Name), Message = "" },
                new ValidationResponse() { PropertyName = nameof(CreateFoodRequest.Calories), Message = "" },
                new ValidationResponse() { PropertyName = nameof(CreateFoodRequest.ProteinGrams), Message = "" },
                new ValidationResponse() { PropertyName = nameof(CreateFoodRequest.CarbohydrateGrams), Message = "" },
                new ValidationResponse() { PropertyName = nameof(CreateFoodRequest.FatGrams), Message = "" },
            }
        };

        _mediator.Send(Arg.Any<CreateFoodCommand>()).Returns(Result.Invalid(new List<ValidationError>()
        {
            new ValidationError() { Identifier =  nameof(CreateFoodCommand.Name), ErrorMessage = "" },
            new ValidationError() { Identifier =  nameof(CreateFoodCommand.Calories), ErrorMessage = "" },
            new ValidationError() { Identifier =  nameof(CreateFoodCommand.ProteinGrams), ErrorMessage = "" },
            new ValidationError() { Identifier =  nameof(CreateFoodCommand.CarbohydrateGrams), ErrorMessage = "" },
            new ValidationError() { Identifier =  nameof(CreateFoodCommand.FatGrams), ErrorMessage = "" },

        }));

        // Act
        var result = (BadRequestObjectResult)await _sut.Create(_invalidRequest, default);

        // Assert
        await _mediator.Received(1).Send(Arg.Is<CreateFoodCommand>(c => c.Name == _invalidRequest.Name));
        result.StatusCode.Should().Be(400);
        result.Value.Should().BeEquivalentTo(expectedResponse);
    }

    [Fact]
    public async Task Create_ShouldReturnProblem_WhenRequestIsValidAndCreationFails()
    {
        // Arrange
        _mediator.Send(Arg.Any<CreateFoodCommand>()).Returns(Result.Error());
        var expectedResponse = new ProblemDetails() { Status = 500 };

        // Act
        var result = (ObjectResult)await _sut.Create(_validRequest, default);

        // Assert
        await _mediator.Received(1).Send(Arg.Is<CreateFoodCommand>(c => c.Name == _validRequest.Name));
        result.StatusCode.Should().Be(500);
        result.Value.Should().BeEquivalentTo(expectedResponse);
    }
}
