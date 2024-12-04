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

    [Fact]
    public void IEnumerableFoods_ShouldMapToFoodsResponseCorrectly()
    {
        // Arrange
        Food banana = new("Banana", 110, 2, 27, 0);
        Food beefJerky = new("Beef Jerky", 80, 15, 5, 0);
        List<Food> foods = [banana, beefJerky];

        FoodsResponse expectedResponse = new()
        {
            Items = new List<FoodResponse>()
            { 
                new FoodResponse()
                {
                    Id = banana.Id,
                    Name = banana.Name,
                    Calories = banana.Calories,
                    ProteinGrams = banana.ProteinGrams,
                    CarbohydrateGrams = banana.CarbohydrateGrams,
                    FatGrams = banana.FatGrams,
                },
                new FoodResponse()
                {
                    Id = beefJerky.Id,
                    Name = beefJerky.Name,
                    Calories = beefJerky.Calories,
                    ProteinGrams = beefJerky.ProteinGrams,
                    CarbohydrateGrams = beefJerky.CarbohydrateGrams,
                    FatGrams = beefJerky.FatGrams,
                },
            }
        };

        // Act
        var result = foods.MapToResponse();

        // Assert
        result.Should().BeEquivalentTo(expectedResponse);
    }
}
