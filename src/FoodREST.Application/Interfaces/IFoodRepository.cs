using FoodREST.Domain;

namespace FoodREST.Application.Interfaces;

public interface IFoodRepository
{
    Task<bool> AddFoodAsync(Food food);
}
