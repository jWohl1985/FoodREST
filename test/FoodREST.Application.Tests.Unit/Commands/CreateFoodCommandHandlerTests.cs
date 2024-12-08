using Ardalis.Result;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Internal;
using FluentValidation.Results;
using FoodREST.Application.Commands;
using FoodREST.Application.Interfaces;
using FoodREST.Domain;
using NSubstitute;
using NSubstitute.ExceptionExtensions;

namespace FoodREST.Application.Tests.Unit.Commands;

public class CreateFoodCommandHandlerTests : IClassFixture<FoodFixture>
{
    private readonly IFoodRepository _foodRepository = Substitute.For<IFoodRepository>();
    private readonly IValidator<CreateFoodCommand> _validator = Substitute.For<IValidator<CreateFoodCommand>>();

    private Food _banana;
    private CreateFoodCommand _command;
    private CreateFoodCommandHandler _sut;

    public CreateFoodCommandHandlerTests(FoodFixture foodFixture)
    {
        _banana = foodFixture.Banana;
        _command = new CreateFoodCommand(_banana.Name, _banana.Calories, _banana.ProteinGrams, _banana.CarbohydrateGrams, _banana.FatGrams);
        _sut = new CreateFoodCommandHandler(_foodRepository, _validator);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenRequestIsValid()
    {
        // Arrange
        _foodRepository.AddFoodAsync(Arg.Any<Food>()).Returns(true);
        _validator.ValidateAsync(_command).Returns(new ValidationResult() { Errors = [] });

        // Act
        var result = await _sut.Handle(_command, default);

        // Assert
        await _foodRepository.Received(1).AddFoodAsync(Arg.Is<Food>(f => f.Name == _banana.Name));
        result.IsError().Should().BeFalse();
        result.Value.Should().BeEquivalentTo(_banana, options => options.Excluding(o => o.Id));
    }

    [Fact]
    public async Task Handle_ShouldReturnError_WhenRequestFails()
    {
        // Arrange
        _foodRepository.AddFoodAsync(Arg.Any<Food>()).Returns(false);

        // Act
        var result = await _sut.Handle(_command, default);

        // Assert
        await _foodRepository.Received(1).AddFoodAsync(Arg.Is<Food>(f => f.Name == _banana.Name));
        result.IsError().Should().BeTrue();
    }

    [Fact]
    public async Task Handle_ShouldThrowValidationException_WhenRequestIsInvalid()
    {
        // Arrange
        var invalidCommand = new CreateFoodCommand(_banana.Name, calories: -5, _banana.ProteinGrams, _banana.CarbohydrateGrams, _banana.FatGrams);
        _validator.ValidateAsync(invalidCommand, options => { options.ThrowOnFailures(); }, default)
            .ThrowsAsyncForAnyArgs(new ValidationException("test"));

        // Act
        var result = () => _sut.Handle(invalidCommand, default);

        // Assert
        await result.Should().ThrowAsync<ValidationException>();
        await _foodRepository.DidNotReceive().AddFoodAsync(Arg.Any<Food>());
    }
}
