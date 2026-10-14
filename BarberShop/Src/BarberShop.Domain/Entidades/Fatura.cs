using BarberShop.Domain.Enums;

namespace BarberShop.Domain.Entidades;

public class Fatura
{
    public long Id { get; set; }
    public DateTime Data { get; set; }
    public string? nota { get; set; }
    public decimal Valor { get; set; }
    public string servico { get; set; } = string.Empty; 
    public PaymentType forma_Pgmt { get; set; }
    public string nome_Cliente { get; set; } = string.Empty;
    public string nome_Barbeiro { get; set; } = string.Empty;
    public Status status  { get; set; }
    public DateTime criado_Em  { get; set; }
    public DateTime? atualizado_Em { get; set; }



}
