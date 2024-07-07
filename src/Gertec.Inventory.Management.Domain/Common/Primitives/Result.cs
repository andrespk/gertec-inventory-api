namespace Gertec.Inventory.Management.Domain.Common.Primitives;

public class Result
{
    private Result(bool isSuccess, BusinessException? exception = default)
    {
        if (exception is not null)
        {
            throw exception;
        }

        IsSuccess = isSuccess;
        Exception = exception;
    }

    public bool IsSuccess { get; }

    public bool IsFailure => !IsSuccess;

    public BusinessException? Exception { get; }

    public static Result Success() => new(true);

    public static Result Failure(BusinessException error) => new(false, error);
}