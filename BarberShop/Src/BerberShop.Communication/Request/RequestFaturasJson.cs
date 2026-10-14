using BarberShop.Communication.Enums;

namespace BarberShop.Communication.Requests;

public class RequestFaturasJson
{
    public DateTime Data { get; set; }
    public string servico { get; set; } = string.Empty;
    public string nomeCliente { get; set; } = string.Empty;
    public string nomeBarbeiro { get; set; } = string.Empty;
    public decimal Valor { get; set; }
    public PaymentType tipoPagamento { get; set; }
    public Status status { get; set; }
    public string? nota { get; set; }
}
