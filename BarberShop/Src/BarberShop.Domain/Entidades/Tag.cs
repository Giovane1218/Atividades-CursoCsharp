namespace BarberShop.Domain.Entidades;

public class Tag
{
    public long Id { get; set; }
    public Enums.Tags Value { get; set; }
    public long FaturaId { get; set; }
    public Fatura Fatura { get; set; } = default!;
}
