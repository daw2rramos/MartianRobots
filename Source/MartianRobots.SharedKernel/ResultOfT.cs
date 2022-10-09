namespace MartianRobots.SharedKernel;

public sealed class Result<T> : Result
{
    private readonly T value;

    public Result(
        T value,
        bool success,
        string error) : base(success, error)
    {
        this.value = value;
    }

    public T Value
    {
        get
        {
            if (!Success)
            {
                throw new InvalidOperationException("The result object has no value.");
            }

            return value;
        }
    }
}