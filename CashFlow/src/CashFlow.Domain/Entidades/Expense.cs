using CashFlow.Domain.Enums;

namespace CashFlow.Domain.Entidades;

public class Expense
{
    public long Id { get; set; }
    public DateTime Data { get; set; }
    public string? Descricao { get; set; }
    public decimal Valor { get; set; }
    public string Titulo { get; set; } = string.Empty; 
    public PaymentType tipoPagamento { get; set; }


}
