namespace MartianRobots.SharedKernel;

public interface IRepository<T>
    where T : class, IAggregateRoot
{
    IUnitOfWork UnitOfWork { get; }

    Task<T?> GetAsync(Guid id, CancellationToken cancellationToken = default);
}