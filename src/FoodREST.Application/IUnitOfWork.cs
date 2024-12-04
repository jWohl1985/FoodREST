namespace FoodREST.Application;

public interface IUnitOfWork
{
    Task SaveChangesAsync();
}
