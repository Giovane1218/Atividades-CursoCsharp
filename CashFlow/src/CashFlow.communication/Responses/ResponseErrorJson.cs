namespace CashFlow.communication.Responses;

public class ResponseErrorJson
{
    public List<string> ErrorMessages { get; set; } = new List<string>();

    public ResponseErrorJson(string errorMessages)
    {
        ErrorMessages = [errorMessages];
    }

    public ResponseErrorJson(List<string> errorMessages)
    {
        ErrorMessages = errorMessages;
    }
}
