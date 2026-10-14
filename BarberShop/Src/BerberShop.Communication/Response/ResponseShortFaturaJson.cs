using BarberShop.Communication.Enums;

namespace BarberShop.Communication.Responses;

public class ResponseShortFaturaJson
{
    public long Id { get; set; }
    public DateTime Data { get; set; }
    public decimal Valor { get; set; }
    public string servico { get; set; } = string.Empty;
    public Status status { get; set; }
}
