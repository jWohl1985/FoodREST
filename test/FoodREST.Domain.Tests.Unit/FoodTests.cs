using FluentAssertions;
using System.Reflection;

namespace FoodREST.Domain.Tests.Unit;

public class FoodTests
{
    [Fact]
    public void Id_ShouldNotBeEmpty()
    {
        // Arrange
        var food = new Food(Guid.NewGuid(), "Banana", 0, 0, 0, 0);

        // Act
        var result = food.Id;

        // Assert
        result.Should().NotBeEmpty();
    }

    [Fact]
    public void Name_ShouldNotBeNull()  
    {
        // Arrange
        var food = () => new Food(Guid.NewGuid(), name: null!, 0, 0, 0, 0);

        // Act

        // Assert
        food.Should().ThrowExactly<ArgumentNullException>();
    }

    [Fact]
    public void Name_CannotBeEmpty()
    {
        // Arrange
        var food = () => new Food(Guid.NewGuid(), name: "", 0, 0, 0, 0);

        // Act

        // Assert
        food.Should().ThrowExactly<ArgumentException>();
    }

    [Fact]
    public void Calories_ShouldNotBeNegative()
    {
        // Arrange
        var food = () => new Food(Guid.NewGuid(), name: "Banana", calories: -15, 0, 0, 0);

        // Act

        // Assert
        food.Should().ThrowExactly<ArgumentException>();
    }

    [Fact]
    public void ProteinGrams_ShouldNotBeNegative()
    {
        // Arrange
        var food = () => new Food(Guid.NewGuid(), name: "Banana", 0, proteinGrams: -1, 0, 0);

        // Act

        // Assert
        food.Should().ThrowExactly<ArgumentException>();
    }

    [Fact]
    public void CarbohydrateGrams_ShouldNotBeNegative()
    {
        // Arrange
        var food = () => new Food(Guid.NewGuid(), name: "Banana", 0, 0, carbohydrateGrams: -1, 0);

        // Act

        // Assert
        food.Should().ThrowExactly<ArgumentException>();
    }

    [Fact]
    public void FatGrams_ShouldNotBeNegative()
    {
        // Arrange
        var food = () => new Food(Guid.NewGuid(), name: "Banana", 0, 0, 0, fatGrams: -1);

        // Act

        // Assert
        food.Should().ThrowExactly<ArgumentException>();
    }
}
