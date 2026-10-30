using BarberShop.Communication.Enums;

namespace BarberShop.Communication.Responses;

public class ResponseFaturaJson
{
    public long Id { get; set; }
    public DateTime Data { get; set; }
    public string Service { get; set; } = string.Empty;
    public string NomeCliente { get; set; } = string.Empty;
    public string NomeBarbeiro { get; set; } = string.Empty;
    public string? Nota { get; set; }
    public decimal Valor { get; set; }
    public PaymentType FormaPgmt { get; set; }
    public Status Status { get; set; }
    public IList<Tags> Tags { get; set; } = [];
    public DateTime CreateOn { get; set; }
    public DateTime? LastUpdateOn { get; set; }
}
