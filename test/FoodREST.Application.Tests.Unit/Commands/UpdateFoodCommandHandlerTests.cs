using Ardalis.Result;
using FluentAssertions;
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

    private UpdateFoodCommand _command;
    private Food _updatedFood;
    private UpdateFoodCommandHandler _sut;
    

    public UpdateFoodCommandHandlerTests()
    {
        _command = new UpdateFoodCommand(Guid.NewGuid(), "Banana", calories: 115, proteinGrams: 2, carbohydrateGrams: 27, fatGrams: 0);
        _updatedFood = new Food(_command.Name, _command.Calories, _command.ProteinGrams, _command.CarbohydrateGrams, _command.FatGrams, _command.Id);
        _sut = new UpdateFoodCommandHandler(_foodRepository, _unitOfWork);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenRequestIsValid()
    {
        // Arrange
        _foodRepository.UpdateFoodAsync(_command.Id, Arg.Any<Food>()).Returns(_updatedFood);

        // Act
        var result = await _sut.Handle(_command, default);

        // Assert
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

        // Act
        var result = await _sut.Handle(_command, default);

        // Assert
        await _foodRepository.Received(1).UpdateFoodAsync(_command.Id, Arg.Is<Food>(f => f.Name == _updatedFood.Name));
        await _unitOfWork.DidNotReceive().SaveChangesAsync();
        result.IsNotFound().Should().BeTrue();
    }
}
