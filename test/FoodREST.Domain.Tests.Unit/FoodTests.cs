using FluentAssertions;
using System.Reflection;

namespace FoodREST.Domain.Tests.Unit;

public class FoodTests
{
    [Fact]
    public void Id_ShouldNotBeEmpty()
    {
        // Arrange
        var food = new Food(null, "Banana", 0, 0, 0, 0);

        // Act
        var result = food.Id;

        // Assert
        result.Should().NotBeEmpty();
        Guid.TryParse(result.ToString(), out Guid _).Should().BeTrue();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void Name_ShouldNotBeNullOrEmpty(string name)  
    {
        // Arrange
        var food = () => new Food(Guid.NewGuid(), name, 0, 0, 0, 0);

        // Act

        // Assert
        food.Should().Throw<ArgumentException>();
    }

    [Theory]
    [InlineData(-5, 10, 10, 10)]
    [InlineData(10, -30, 10, 10)]
    [InlineData(10, 10, -100, 10)]
    [InlineData(5, 5, 5, -3)]
    public void NutritionInfo_ShouldNotBeNegative(int calories, int protein, int carbs, int fat)
    {
        // Arrange
        var food = () => new Food(Guid.NewGuid(), "Test", calories, protein, carbs, fat);

        // Act

        // Assert
        food.Should().ThrowExactly<ArgumentException>();
    }
}
