using BarberShop.Communication.Enums;

namespace BarberShop.Communication.Responses;

public class ResponseShortFaturaJson
{
    public string Service { get; set; } = string.Empty;
    public DateTime Data { get; set; }
    public decimal Valor { get; set; }
    public Status Status { get; set; }
    public long Id { get; set; }
}
