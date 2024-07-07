namespace Gertec.Inventory.Management.Domain.Common.Primitives;

public abstract class BusinessException : Exception
{
    public BusinessException(string message) : base(message)
    {
        
    }

    protected BusinessException()
    {
        
    }
}