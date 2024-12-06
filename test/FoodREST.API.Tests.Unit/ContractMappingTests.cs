using FluentAssertions;
using FoodREST.API.Mapping;
using FoodREST.Contracts.Responses;
using FoodREST.Domain;

namespace FoodREST.API.Tests.Unit;

public class ContractMappingTests : IClassFixture<FoodFixture>
{
    private Food _banana;
    private Food _beefJerky;

    public ContractMappingTests(FoodFixture foodFixture)
    {
        _banana = foodFixture.Banana;
        _beefJerky = foodFixture.BeefJerky;
    }

    [Fact]
    public void Food_ShouldMapToFoodResponseCorrectly()
    {
        // Arrange
        FoodResponse expectedResponse = new()
        {
            Id = _banana.Id,
            Name = _banana.Name,
            Calories = _banana.Calories,
            ProteinGrams = _banana.ProteinGrams,
            CarbohydrateGrams = _banana.CarbohydrateGrams,
            FatGrams = _banana.FatGrams,
        };

        // Act
        var result = _banana.MapToResponse();

        // Assert
        result.Should().BeEquivalentTo(expectedResponse);
    }

    [Fact]
    public void IEnumerableFoods_ShouldMapToFoodsResponseCorrectly()
    {
        // Arrange
        List<Food> foods = [_banana, _beefJerky];

        FoodsResponse expectedResponse = new()
        {
            Items = new List<FoodResponse>()
            { 
                new FoodResponse()
                {
                    Id = _banana.Id,
                    Name = _banana.Name,
                    Calories = _banana.Calories,
                    ProteinGrams = _banana.ProteinGrams,
                    CarbohydrateGrams = _banana.CarbohydrateGrams,
                    FatGrams = _banana.FatGrams,
                },
                new FoodResponse()
                {
                    Id = _beefJerky.Id,
                    Name = _beefJerky.Name,
                    Calories = _beefJerky.Calories,
                    ProteinGrams = _beefJerky.ProteinGrams,
                    CarbohydrateGrams = _beefJerky.CarbohydrateGrams,
                    FatGrams = _beefJerky.FatGrams,
                },
            }
        };

        // Act
        var result = foods.MapToResponse();

        // Assert
        result.Should().BeEquivalentTo(expectedResponse);
    }
}
