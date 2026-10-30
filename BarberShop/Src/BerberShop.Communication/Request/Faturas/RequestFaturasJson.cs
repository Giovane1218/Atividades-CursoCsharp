using BarberShop.Communication.Enums;

namespace BarberShop.Communication.Requests;

public class RequestFaturasJson
{
    public DateTime Data { get; set; }
    public string Service { get; set; } = string.Empty;
    public string NomeCliente { get; set; } = string.Empty;
    public string NomeBarbeiro { get; set; } = string.Empty;
    public decimal Valor { get; set; }
    public PaymentType FormaPgmt { get; set; }
    public Status Status { get; set; }
    public string? Nota { get; set; }
    public IList<Tags> Tags { get; set; } = [];
}
