using MediatR;

namespace MartianRobots.SharedKernel;

public abstract class BaseEntity
{
    protected BaseEntity()
    {
    }

    protected BaseEntity(Guid id)
        : this()
    {
        Id = id;
    }    

    public Guid Id { get; protected set; } = Guid.NewGuid();

    public bool SoftDeleted { get; protected set; }
}