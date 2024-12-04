using FoodREST.Application.Interfaces;
using FoodREST.Domain;

namespace FoodREST.Infrastructure.Services;

public class InMemoryFoodRepository : IFoodRepository
{
    private List<Food> _foods = new();

    public Task<bool> AddFoodAsync(Food food)
    {
        _foods.Add(food);
        return Task.FromResult(true);
    }
}
