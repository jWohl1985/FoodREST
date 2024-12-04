using FluentAssertions;
using FoodREST.Application.Commands;

namespace FoodREST.Application.Tests.Unit;

public class CreateFoodCommandTests
{
    [Fact]
    public void Name_CanNotBeNull()
    {
        // Arrange
        var create = () => new CreateFoodCommand(name: null!, 0, 0, 0, 0);

        // Act

        // Assert
        create.Should().ThrowExactly<ArgumentNullException>();
    }

    [Fact]
    public void Calories_CanNotBeNegative()
    {
        // Arrange
        var create = () => new CreateFoodCommand(name: "Banana", calories: -1, proteinGrams: 0, carbohydrateGrams: 0, fatGrams: 0);

        // Act

        // Assert
        create.Should().ThrowExactly<ArgumentException>();
    }

    [Fact]
    public void ProteinGrams_CanNotBeNegative()
    {
        // Arrange
        var create = () => new CreateFoodCommand(name: "Banana", calories: 0, proteinGrams: -1, carbohydrateGrams: 0, fatGrams: 0);

        // Act

        // Assert
        create.Should().ThrowExactly<ArgumentException>();
    }

    [Fact]
    public void CarbohydrateGrams_CanNotBeNegative()
    {
        // Arrange
        var create = () => new CreateFoodCommand(name: "Banana", calories: 0, proteinGrams: 0, carbohydrateGrams: -1, fatGrams: 0);

        // Act

        // Assert
        create.Should().ThrowExactly<ArgumentException>();
    }

    [Fact]
    public void FatGrams_CanNotBeNegative()
    {
        // Arrange
        var create = () => new CreateFoodCommand(name: "Banana", calories: 0, proteinGrams: 0, carbohydrateGrams: 0, fatGrams: -1);

        // Act

        // Assert
        create.Should().ThrowExactly<ArgumentException>();
    }
}
