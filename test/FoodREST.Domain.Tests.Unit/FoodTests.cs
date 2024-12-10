using FluentAssertions;

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
}
