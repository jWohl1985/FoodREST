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

    public Task<Food?> GetByIdAsync(Guid id)
    {
        Food? food = _foods.FirstOrDefault(f => f.Id == id);
        return Task.FromResult(food); 
    }

    public Task<IEnumerable<Food>> GetAllAsync()
    {
        return Task.FromResult(_foods.AsEnumerable());
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
