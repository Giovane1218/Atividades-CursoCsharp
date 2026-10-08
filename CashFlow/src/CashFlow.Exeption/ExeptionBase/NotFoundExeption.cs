using System.Net;

namespace CashFlow.Exeption.ExeptionBase;

public class NotFoundExeption : CashFlowExeption
{
    public NotFoundExeption(string message) : base(message)
    {
        
    }

    public override int StatusCode => (int)HttpStatusCode.NotFound;

    public override List<string> GetErrors()
    {
        return [Message];
    }
}
