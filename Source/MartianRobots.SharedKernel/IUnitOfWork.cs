namespace MartianRobots.SharedKernel;

public interface IUnitOfWork
{
    Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default);    
}