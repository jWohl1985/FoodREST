using FoodREST.Domain;

namespace FoodREST.Application.Interfaces;

public interface IFoodRepository
{
    Task<bool> AddFoodAsync(Food food);

    Task<Food?> GetByIdAsync(Guid id);

    Task<IEnumerable<Food>> GetAllAsync();

    Task<Food?> UpdateFoodAsync(Guid id, Food food);

    Task<bool> DeleteFoodAsync(Guid id);
}
