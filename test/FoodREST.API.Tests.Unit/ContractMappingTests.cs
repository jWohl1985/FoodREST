using FluentAssertions;
using FoodREST.API.Mapping;
using FoodREST.Contracts.Responses;
using FoodREST.Domain;

namespace FoodREST.API.Tests.Unit;

public class ContractMappingTests
{
    [Fact]
    public void Food_ShouldMapToFoodResponseCorrectly()
    {
        // Arrange
        Food food = new("Banana", 110, 2, 27, 0);

        FoodResponse expectedResponse = new()
        {
            Id = food.Id,
            Name = food.Name,
            Calories = food.Calories,
            ProteinGrams = food.ProteinGrams,
            CarbohydrateGrams = food.CarbohydrateGrams,
            FatGrams = food.FatGrams,
        };

        // Act
        var result = food.MapToResponse();

        // Assert
        result.Should().BeEquivalentTo(expectedResponse);
    }
}
