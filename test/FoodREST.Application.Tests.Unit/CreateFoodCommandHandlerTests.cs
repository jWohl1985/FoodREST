using Ardalis.Result;
using FluentAssertions;
using FoodREST.Domain;
using NSubstitute;

namespace FoodREST.Application.Tests.Unit;

public class CreateFoodCommandHandlerTests
{
    private readonly IFoodRepository _foodRepository = Substitute.For<IFoodRepository>();
    private readonly IUnitOfWork _unitOfWork = Substitute.For<IUnitOfWork>();

    private CreateFoodCommandHandler _sut;

    public CreateFoodCommandHandlerTests()
    {
        _sut = new CreateFoodCommandHandler(_foodRepository, _unitOfWork);
    }

    [Fact]
    public async void Handle_ShouldReturnSuccess_WhenRequestIsValid()
    {
        // Arrange
        var command = new CreateFoodCommand("Banana", calories: 115, proteinGrams: 2, carbohydrateGrams: 27, fatGrams: 0);
        var expectedFood = new Food(command.Name, command.Calories, command.ProteinGrams, command.CarbohydrateGrams, command.FatGrams);
        _foodRepository.AddFoodAsync(Arg.Any<Food>()).Returns(true);

        // Act
        var result = await _sut.Handle(command, default);

        // Assert
        await _foodRepository.Received(1).AddFoodAsync(Arg.Is<Food>(f => f.Name == "Banana"));
        await _unitOfWork.Received(1).SaveChangesAsync();
        result.Should().BeEquivalentTo(Result.Success());
    }

    [Fact]
    public async void Handle_ShouldReturnError_WhenRequestFails()
    {
        // Arrange
        var command = new CreateFoodCommand("Banana", calories: 115, proteinGrams: 2, carbohydrateGrams: 27, fatGrams: 0);
        var expectedFood = new Food(command.Name, command.Calories, command.ProteinGrams, command.CarbohydrateGrams, command.FatGrams);
        _foodRepository.AddFoodAsync(Arg.Any<Food>()).Returns(false);

        // Act
        var result = await _sut.Handle(command, default);

        // Assert
        await _foodRepository.Received(1).AddFoodAsync(Arg.Is<Food>(f => f.Name == "Banana"));
        await _unitOfWork.DidNotReceive().SaveChangesAsync();
        result.Should().BeEquivalentTo(Result.Error());
    }
}
