using FoodREST.Domain;

namespace FoodREST.Application.Interfaces;

public interface IFoodRepository
{
    Task<bool> AddFoodAsync(Food food, CancellationToken token = default);

    Task<Food?> GetByIdAsync(Guid id, CancellationToken token = default);

    Task<IEnumerable<Food>> GetAllAsync(CancellationToken token = default);

    Task<Food?> UpdateFoodAsync(Guid id, Food food, CancellationToken token = default);

    Task<bool> DeleteFoodAsync(Guid id, CancellationToken token = default);
}
