namespace FoodREST.Application.Interfaces;

public interface IUnitOfWork
{
    bool HasStarted { get; }

    Task BeginAsync(CancellationToken token = default);

    void Rollback();

    void SaveChanges();
}
