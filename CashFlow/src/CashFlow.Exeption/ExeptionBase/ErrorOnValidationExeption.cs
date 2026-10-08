using System.Net;

namespace CashFlow.Exeption.ExeptionBase;

public class ErrorOnValidationExeption : CashFlowExeption
{
    private readonly List<string> _errors;

    public override int StatusCode => (int)HttpStatusCode.BadRequest;

    public ErrorOnValidationExeption(List<string> errorMessages) : base(string.Empty)
    {
        _errors = errorMessages;
    }

    public override List<string> GetErrors()
    {
        return _errors;
    }
}
