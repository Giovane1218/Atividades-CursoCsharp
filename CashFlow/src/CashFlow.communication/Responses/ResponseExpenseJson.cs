using CashFlow.communication.Enums;

namespace CashFlow.communication.Responses;

public class ResponseExpenseJson
{
    public long Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string? Descricao { get; set; }
    public decimal Valor { get; set; }
    public DateTime Data { get; set; }
    public PaymentType tipoPagamento { get; set; }
}
