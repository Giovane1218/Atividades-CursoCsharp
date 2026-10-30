namespace BarberShop.Communication.Responses;

public class ResponseRegisterFaturaJson
{
    public long Id { get; set; }
    public string Service { get; set; } = string.Empty;
    public string NomeBarbeiro { get; set; } = string.Empty;
}
