using Ardalis.Result;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using FoodREST.Application.Commands;
using FoodREST.Application.Interfaces;
using FoodREST.Domain;
using NSubstitute;

namespace FoodREST.Application.Tests.Unit.Commands;

public class CreateFoodCommandHandlerTests
{
    private readonly IFoodRepository _foodRepository = Substitute.For<IFoodRepository>();
    private readonly IUnitOfWork _unitOfWork = Substitute.For<IUnitOfWork>();
    private readonly IValidator<CreateFoodCommand> _validator = Substitute.For<IValidator<CreateFoodCommand>>();

    private CreateFoodCommand _command;
    private Food _expectedFood;
    private CreateFoodCommandHandler _sut;

    public CreateFoodCommandHandlerTests()
    {
        _command = new CreateFoodCommand(name: "Banana", calories: 115, proteinGrams: 2, carbohydrateGrams: 27, fatGrams: 0);
        _expectedFood = new Food(null, _command.Name, _command.Calories, _command.ProteinGrams, _command.CarbohydrateGrams, _command.FatGrams);
        _sut = new CreateFoodCommandHandler(_foodRepository, _unitOfWork, _validator);
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
        await _foodRepository.Received(1).AddFoodAsync(Arg.Is<Food>(f => f.Name == "Banana"));
        await _unitOfWork.Received(1).SaveChangesAsync();
        result.IsError().Should().BeFalse();
        result.Value.Should().BeEquivalentTo(_expectedFood, options => options.Excluding(o => o.Id));
    }

    [Fact]
    public async Task Handle_ShouldReturnError_WhenRequestFails()
    {
        // Arrange
        _foodRepository.AddFoodAsync(Arg.Any<Food>()).Returns(false);
        _validator.ValidateAsync(_command).Returns(new ValidationResult() { Errors = [] });

        // Act
        var result = await _sut.Handle(_command, default);

        // Assert
        await _foodRepository.Received(1).AddFoodAsync(Arg.Is<Food>(f => f.Name == "Banana"));
        await _unitOfWork.DidNotReceive().SaveChangesAsync();
        result.IsError().Should().BeTrue();
    }

    [Fact]
    public async Task Handle_ShouldReturnValidationError_WhenRequestIsInvalid()
    {
        // Arrange
        var invalidCommand = new CreateFoodCommand("Banana", -5, 0, 0, 0);
        _validator.ValidateAsync(invalidCommand, default)
            .Returns(new ValidationResult([ new ValidationFailure(propertyName: "Calories", errorMessage: "Cannot be negative")]));

        // Act
        var result = await _sut.Handle(invalidCommand, default);

        // Assert
        await _validator.Received(1).ValidateAsync(invalidCommand, default);
        await _foodRepository.DidNotReceive().AddFoodAsync(Arg.Any<Food>());
        await _unitOfWork.DidNotReceive().SaveChangesAsync();
        result.IsInvalid().Should().BeTrue();
    }
}
