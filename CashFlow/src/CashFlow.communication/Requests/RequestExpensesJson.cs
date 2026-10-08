using CashFlow.communication.Enums;

namespace CashFlow.communication.Requests;

public class RequestExpensesJson
{
    public string Titulo { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public DateTime Data { get; set; }
    public decimal Valor { get; set; }
    public PaymentType PaymentType { get; set;} 
}
