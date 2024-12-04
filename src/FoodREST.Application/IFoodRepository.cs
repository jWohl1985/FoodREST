using FoodREST.Domain;

namespace FoodREST.Application;

public interface IFoodRepository
{
    Task<bool> AddFoodAsync(Food food);
}
