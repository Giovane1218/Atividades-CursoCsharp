using BarberShop.Domain.Enums;

namespace BarberShop.Domain.Entidades;

public class Fatura
{
    public long Id { get; set; }
    public DateTime Data { get; set; }
    public string? Nota { get; set; }
    public decimal Valor { get; set; }
    public string Service { get; set; } = string.Empty; 
    public PaymentType FormaPgmt { get; set; }
    public string NomeCliente { get; set; } = string.Empty;
    public string NomeBarbeiro { get; set; } = string.Empty;
    public Status Status  { get; set; }
    public ICollection<Tag> Tags { get; set; } = [];
    public DateTime CreateOn  { get; set; }
    public DateTime? LastUpdateOn{ get; set; }
    public long UserId { get; set; }
    public Usuario User { get; set; } = default!;
}
