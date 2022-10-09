namespace MartianRobots.SharedKernel;

public class Result
{
    protected Result(
        bool success,
        string error)
    {
        Success = success;
        Error = error;
    }

    public string Error { get; }

    public bool Success { get; }

    public bool Failure => !Success;

    public static Result Ok()
    {
        return new(true, string.Empty);
    }

    public static Result Fail(string error)
    {
        return new(false, error);
    }

    public static Result<T> Ok<T>(T value)
    {
        return new(value, true, string.Empty);
    }

    public static Result<T> Fail<T>(string error)
    {
        return new(default!, false, error);
    }
}