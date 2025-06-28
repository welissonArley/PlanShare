namespace PlanShare.App.Models.ValueObjects;
public class Result
{
    public bool IsSuccess { get; private set; }
    public IList<string>? ErrorMessages { get; private set; }

    protected Result()
    {
        IsSuccess = true;
    }

    protected Result(IList<string> errorMessages)
    {
        ErrorMessages = errorMessages;
        IsSuccess = false;
    }

    public static Result Success() => new();
    public static Result Failure(IList<string> errorMessages) => new(errorMessages);
}

public class Result<TResponse> : Result
{
    public TResponse? Response { get; private set; }

    protected Result(TResponse response) : base()
    {
        Response = response;
    }

    protected Result(IList<string> errorMessages) : base(errorMessages)
    {
    }

    public static Result<TResponse> Success(TResponse response) => new(response);

    public static new Result<TResponse> Failure(IList<string> errorMessages) => new(errorMessages);
}