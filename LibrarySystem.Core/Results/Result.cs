namespace LibrarySystem.Core.Results;

public class Result
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public Error Error { get; }

    protected Result(bool isSuccess, Error error)
    {
        IsSuccess = isSuccess;
        Error = error;
    }
    public static Result Success() => new(true, Error.None);
    public static Result Failure(Error error) => new(false, error);
}
public class Result<TValue> : Result
{
    public TValue? Value { get; }

    private Result(TValue? value, bool isSuccess, Error error) : base(isSuccess, error)
    {
        Value = value;
    }
    public static Result<TValue> Success(TValue value) => new(value, true, Error.None);
    public new static Result<TValue> Failure(Error error) => new(default, false, error);
}