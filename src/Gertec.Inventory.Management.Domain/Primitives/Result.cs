namespace Gertec.Inventory.Management.Domain.Primitives;

public class Result
{
    private Result(bool isSuccess, BusinessException exception)
    {
        if (hasValidationFailed(isSuccess, exception))
        {
            throw new ArgumentException(exception.Description, nameof(exception));
        }

        IsSuccess = isSuccess;
        Exception = exception;
    }

    public bool IsSuccess { get; }

    public bool IsFailure => !IsSuccess;

    public BusinessException Exception { get; }

    public static Result Success() => new(true, BusinessException.None);

    public static Result Failure(BusinessException error) => new(false, error);

    private bool hasValidationFailed(bool isSuccess, BusinessException exception)
        => isSuccess && exception != BusinessException.None ||
           !isSuccess && exception == BusinessException.None;
}