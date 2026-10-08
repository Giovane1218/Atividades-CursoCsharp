namespace CashFlow.communication.Responses;

public class ResponseShortExpenseJson
{
    public long Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public decimal Valor { get; set; }
}