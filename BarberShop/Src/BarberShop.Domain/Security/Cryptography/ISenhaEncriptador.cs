namespace BarberShop.Domain.Security.Cryptography;

public interface ISenhaEncriptador
{
    string Encrypt(string senha);
    bool Verify(string senha, string senhaHash);
}
