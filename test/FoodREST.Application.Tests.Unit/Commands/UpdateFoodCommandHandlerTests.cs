using Ardalis.Result;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using FoodREST.Application.Commands;
using FoodREST.Application.Interfaces;
using FoodREST.Domain;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace FoodREST.Application.Tests.Unit.Commands;

public class UpdateFoodCommandHandlerTests
{
    private readonly IFoodRepository _foodRepository = Substitute.For<IFoodRepository>();
    private readonly IUnitOfWork _unitOfWork = Substitute.For<IUnitOfWork>();
    private readonly IValidator<UpdateFoodCommand> _validator = Substitute.For<IValidator<UpdateFoodCommand>>();

    private UpdateFoodCommand _command;
    private Food _updatedFood;
    private UpdateFoodCommandHandler _sut;
    

    public UpdateFoodCommandHandlerTests()
    {
        _command = new UpdateFoodCommand(Guid.NewGuid(), "Banana", calories: 115, proteinGrams: 2, carbohydrateGrams: 27, fatGrams: 0);
        _updatedFood = new Food(_command.Id, _command.Name, _command.Calories, _command.ProteinGrams, _command.CarbohydrateGrams, _command.FatGrams);
        _sut = new UpdateFoodCommandHandler(_foodRepository, _unitOfWork, _validator);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenRequestIsValid()
    {
        // Arrange
        _foodRepository.UpdateFoodAsync(_command.Id, Arg.Any<Food>()).Returns(_updatedFood);
        _validator.ValidateAsync(_command).Returns(new ValidationResult() { Errors = [] });

        // Act
        var result = await _sut.Handle(_command, default);

        // Assert
        await _validator.Received(1).ValidateAsync(_command);
        await _foodRepository.Received(1).UpdateFoodAsync(_command.Id, Arg.Is<Food>(f => f.Name == _updatedFood.Name));
        await _unitOfWork.Received(1).SaveChangesAsync();
        result.IsError().Should().BeFalse();
        result.Value.Should().BeEquivalentTo(_updatedFood);
    }

    [Fact]
    public async Task Handle_ShouldReturnError_WhenRequestFails()
    {
        // Arrange
        _foodRepository.UpdateFoodAsync(Arg.Any<Guid>(), Arg.Any<Food>()).ReturnsNull();
        _validator.ValidateAsync(_command).Returns(new ValidationResult() { Errors = [] });

        // Act
        var result = await _sut.Handle(_command, default);

        // Assert
        await _validator.Received(1).ValidateAsync(_command);
        await _foodRepository.Received(1).UpdateFoodAsync(_command.Id, Arg.Is<Food>(f => f.Name == _updatedFood.Name));
        await _unitOfWork.DidNotReceive().SaveChangesAsync();
        result.IsNotFound().Should().BeTrue();
    }

    [Fact]
    public async Task Handle_ShouldReturnValidationError_WhenRequestIsInvalid()
    {
        // Arrange
        var invalidCommand = new UpdateFoodCommand(Guid.NewGuid(), string.Empty, 15, 5, 1, 2);
        _foodRepository.UpdateFoodAsync(invalidCommand.Id, Arg.Any<Food>()).ReturnsNull();
        _validator.ValidateAsync(invalidCommand, default)
            .Returns(new ValidationResult([ new ValidationFailure(propertyName: "Name", errorMessage: "Cannot be empty")]));

        // Act
        var result = await _sut.Handle(invalidCommand, default);

        // Assert
        await _validator.Received(1).ValidateAsync(invalidCommand, default);
        await _foodRepository.DidNotReceive().UpdateFoodAsync(Arg.Any<Guid>(), Arg.Any<Food>());
        await _unitOfWork.DidNotReceive().SaveChangesAsync();
        result.IsInvalid().Should().BeTrue();
    }
}
