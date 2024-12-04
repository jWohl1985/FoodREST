namespace FoodREST.Application.Interfaces;

public interface IUnitOfWork
{
    Task SaveChangesAsync();
}
