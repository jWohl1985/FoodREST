using FoodREST.Application.Interfaces;

namespace FoodREST.Infrastructure.Services;

public class FakeUnitOfWork : IUnitOfWork
{
    public Task SaveChangesAsync()
    {
        return Task.CompletedTask;
    }
}
