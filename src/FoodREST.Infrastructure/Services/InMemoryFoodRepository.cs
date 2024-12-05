using FoodREST.Application.Interfaces;
using FoodREST.Domain;

namespace FoodREST.Infrastructure.Services;

public class InMemoryFoodRepository : IFoodRepository
{
    private List<Food> _foods = new();

    public async Task<bool> AddFoodAsync(Food food)
    {
        _foods.Add(food);
        return await Task.FromResult(true);
    }

    public async Task<Food?> GetByIdAsync(Guid id)
    {
        Food? food = _foods.FirstOrDefault(f => f.Id == id);
        return await Task.FromResult(food); 
    }

    public async Task<IEnumerable<Food>> GetAllAsync()
    {
        return await Task.FromResult(_foods.AsEnumerable());
    }

    public async Task<Food?> UpdateFoodAsync(Guid id, Food food)
    {
        Food? existingFood = _foods.FirstOrDefault(f => f.Id == id);

        if (existingFood is null)
        {
            return null;
        }

        _foods.Remove(existingFood);
        _foods.Add(food);

        return await Task.FromResult(food);
    }

    public async Task<bool> DeleteFoodAsync(Guid id)
    {
        Food? food = _foods.FirstOrDefault(f => f.Id == id);

        if (food is null)
        {
            return await Task.FromResult(false);
        }

        _foods.Remove(food);
        return await Task.FromResult(true);
    }
}
