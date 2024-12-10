using FoodREST.Application.Interfaces;
using FoodREST.Application.Queries;
using FoodREST.Domain;
using FoodREST.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace FoodREST.Infrastructure.Services;

internal sealed class EFCoreFoodRepository : IFoodRepository
{
    private readonly FoodContext _context;

    public EFCoreFoodRepository(FoodContext context)
    {
        _context = context;
    }

    public async Task<bool> AddFoodAsync(Food food, CancellationToken token = default)
    {
        var result = await _context.Foods.AddAsync(food, token);

        await _context.SaveChangesAsync(token);

        return result is not null;
    }

    public async Task<Food?> GetByIdAsync(Guid id, CancellationToken token = default)
    {
        var food = await _context.Foods.FirstOrDefaultAsync(f => f.Id == id, token);
        return food;
    }

    public async Task<IEnumerable<Food>> GetAllAsync(GetAllFoodsOptions options, CancellationToken token = default)
    {
        IQueryable<Food> allMatches = _context.Foods.Where(f => (options.Name == null) || f.Name.Contains(options.Name));

        if (options.SortField is not null && options.SortField == "name")
        {
            allMatches = options.SortOrder == SortOrder.Ascending
                ? allMatches.OrderBy(f => f.Name)
                : allMatches.OrderByDescending(f => f.Name);
        }

        var result = await allMatches
            .Skip((options.Page - 1) * options.PageSize)
            .Take(options.PageSize)
            .ToListAsync(token);

        return result;
    }

    public async Task<int> GetCountAsync(GetAllFoodsOptions options, CancellationToken token = default)
    {
        IQueryable<Food> allMatches = _context.Foods.Where(f => (options.Name == null) || f.Name.Contains(options.Name));

        return await allMatches.CountAsync(token);
    }

    public async Task<Food?> UpdateFoodAsync(Guid id, Food food, CancellationToken token = default)
    {
        Food? existingFood = await _context.Foods.Where(f => f.Id == id).FirstOrDefaultAsync(token);

        if (existingFood is null)
        {
            return null;
        }

        existingFood.Name = food.Name;
        existingFood.Calories = food.Calories;
        existingFood.ProteinGrams = food.ProteinGrams;
        existingFood.CarbohydrateGrams = food.CarbohydrateGrams;
        existingFood.FatGrams = food.FatGrams;

        _context.Foods.Update(existingFood);
        await _context.SaveChangesAsync(token);

        return existingFood;
    }

    public async Task<bool> DeleteFoodAsync(Guid id, CancellationToken token = default)
    {
        Food? food = await _context.Foods.FirstOrDefaultAsync(f => f.Id == id, token);

        if (food is null)
        {
            return false;
        }

        _context.Foods.Remove(food);
        await _context.SaveChangesAsync(token);
        return true;
    }
}
