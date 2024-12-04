using FluentAssertions;
using System.Reflection;

namespace FoodREST.Domain.Tests.Unit;

public class FoodTests
{
    [Fact]
    public void Id_ShouldNotBeEmpty()
    {
        // Arrange
        var food = new Food(name: "Banana");

        // Act
        var result = food.Id;

        // Assert
        result.Should().NotBeEmpty();
    }

    [Fact]
    public void Name_ShouldNotBeNull()
    {
        // Arrange
        var food = () => new Food(name: null!);

        // Act

        // Assert
        food.Should().ThrowExactly<ArgumentNullException>();
    }

    [Fact]
    public void Name_CannotBeEmpty()
    {
        // Arrange
        var food = () => new Food(name: "");

        // Act

        // Assert
        food.Should().ThrowExactly<ArgumentException>();
    }

    [Fact]
    public void Calories_ShouldNotBeNegative()
    {
        // Arrange
        var food = () => new Food(name: "Banana", calories: -15);

        // Act

        // Assert
        food.Should().ThrowExactly<ArgumentException>();
    }

    [Fact]
    public void ProteinGrams_ShouldNotBeNegative()
    {
        // Arrange
        var food = () => new Food(name: "Banana", proteinGrams: -1);

        // Act

        // Assert
        food.Should().ThrowExactly<ArgumentException>();
    }

    [Fact]
    public void CarbohydrateGrams_ShouldNotBeNegative()
    {
        // Arrange
        var food = () => new Food(name: "Banana", carbohydrateGrams: -1);

        // Act

        // Assert
        food.Should().ThrowExactly<ArgumentException>();
    }

    [Fact]
    public void FatGrams_ShouldNotBeNegative()
    {
        // Arrange
        var food = () => new Food(name: "Banana", fatGrams: -1);

        // Act

        // Assert
        food.Should().ThrowExactly<ArgumentException>();
    }

    [Fact]
    public void NutritionInfo_ShouldDefaultToZero_WhenNotSupplied()
    {
        // Arrange
        var food = new Food(name: "Banana");

        // Act

        // Assert
        food.Calories.Should().Be(0);
        food.ProteinGrams.Should().Be(0);
        food.CarbohydrateGrams.Should().Be(0);
        food.FatGrams.Should().Be(0);
    }
}
