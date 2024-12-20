﻿using Ardalis.Result;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using FoodREST.Application.Commands;
using FoodREST.Application.Interfaces;
using FoodREST.Domain;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NSubstitute.ReturnsExtensions;

namespace FoodREST.Application.Tests.Unit.Commands;

public class UpdateFoodCommandHandlerTests : IClassFixture<FoodFixture>
{
    private readonly IFoodRepository _foodRepository = Substitute.For<IFoodRepository>();
    private readonly IValidator<UpdateFoodCommand> _validator = Substitute.For<IValidator<UpdateFoodCommand>>();

    private UpdateFoodCommand _command;
    private Food _expectedFood;
    private UpdateFoodCommandHandler _sut;
    

    public UpdateFoodCommandHandlerTests(FoodFixture foodFixture)
    {
        Food beefJerky = foodFixture.BeefJerky;
        Food banana = foodFixture.Banana;

        // Update the beefJerky to have the banana info (use the beef jerky Id, but banana properties)
        _command = new UpdateFoodCommand(beefJerky.Id, banana.Name, banana.Calories, banana.ProteinGrams, banana.CarbohydrateGrams, banana.FatGrams);
        _expectedFood = new Food(_command.Id, _command.Name, _command.Calories, _command.ProteinGrams, _command.CarbohydrateGrams, _command.FatGrams);

        _sut = new UpdateFoodCommandHandler(_foodRepository, _validator);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenRequestIsValid()
    {
        // Arrange
        _foodRepository.UpdateFoodAsync(_command.Id, Arg.Any<Food>()).Returns(_expectedFood);

        // Act
        var result = await _sut.Handle(_command, default);

        // Assert
        await _foodRepository.Received(1).UpdateFoodAsync(_command.Id, Arg.Is<Food>(f => f.Name == _expectedFood.Name));
        result.IsError().Should().BeFalse();
        result.Value.Should().BeEquivalentTo(_expectedFood);
    }

    [Fact]
    public async Task Handle_ShouldReturnError_WhenRequestFails()
    {
        // Arrange
        _foodRepository.UpdateFoodAsync(Arg.Any<Guid>(), Arg.Any<Food>()).ReturnsNull();

        // Act
        var result = await _sut.Handle(_command, default);

        // Assert
        await _foodRepository.Received(1).UpdateFoodAsync(_command.Id, Arg.Is<Food>(f => f.Name == _expectedFood.Name));
        result.IsNotFound().Should().BeTrue();
    }

    [Fact]
    public async Task Handle_ShouldThrowValidationException_WhenRequestIsInvalid()
    {
        // Arrange
        var invalidCommand = new UpdateFoodCommand(_command.Id, string.Empty, _command.Calories, _command.ProteinGrams, _command.CarbohydrateGrams, _command.FatGrams);
        _foodRepository.UpdateFoodAsync(invalidCommand.Id, Arg.Any<Food>()).ReturnsNull();
        _validator.ValidateAsync(invalidCommand, options => { options.ThrowOnFailures(); }, default)
            .ThrowsAsyncForAnyArgs(new ValidationException(message: "test"));

        // Act
        var result = () => _sut.Handle(invalidCommand, default);

        // Assert
        await result.Should().ThrowAsync<ValidationException>();
        await _foodRepository.DidNotReceive().UpdateFoodAsync(Arg.Any<Guid>(), Arg.Any<Food>());

    }
}
