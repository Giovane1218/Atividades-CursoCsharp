namespace BarberShop.Communication.Response.Users;

public class ResponseUserJson
{
    public long Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public DateTime CreateOn { get; set; }
    public DateTime? LastUpdateOn { get; set; }

}
