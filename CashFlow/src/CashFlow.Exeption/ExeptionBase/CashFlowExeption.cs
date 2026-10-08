namespace CashFlow.Exeption.ExeptionBase;

public abstract class CashFlowExeption : SystemException
{
    protected CashFlowExeption(string message) : base(message)
    {
        
    }

    public abstract int StatusCode { get; }
    public abstract List<string> GetErrors();
}
