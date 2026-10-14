using BarberShop.Communication.Enums;

namespace BarberShop.Communication.Responses;

public class ResponseFaturaJson
{
    public long Id { get; set; }
    public DateTime Data { get; set; }
    public string servico { get; set; } = string.Empty;
    public string nomeCliente { get; set; } = string.Empty;
    public string nomeBarbeiro { get; set; } = string.Empty;
    public string? nota { get; set; }
    public decimal Valor { get; set; }
    public PaymentType tipoPagamento { get; set; }
    public Status status { get; set; }
    public DateTime criado_Em { get; set; }
    public DateTime? atualizadoEm { get; set; }
}
