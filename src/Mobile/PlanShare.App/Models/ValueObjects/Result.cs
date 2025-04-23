namespace PlanShare.App.Models.ValueObjects;
public class Result
{
    public bool IsSuccess { get; private set; }
    public IList<string>? ErrorMessages { get; private set; }

    public static Result Success() => new() { IsSuccess = true };
    public static Result Failure(IList<string> errorMessages) => new() { IsSuccess = false, ErrorMessages = errorMessages };
}
