using BarberShop.Domain.Security.Cryptography;
using BC = BCrypt.Net.BCrypt;
namespace BarberShop.Infraestrura.Security.Cryptografy;

public class BCrypt : ISenhaEncriptador
{
    public string Encrypt(string senha)
    {
        string senhaHash = BC.HashPassword(senha);


        return senhaHash;
    }

    public bool Verify(string senha, string senhaHash)
    {
        return BC.Verify(senha, senhaHash);
    }
}
