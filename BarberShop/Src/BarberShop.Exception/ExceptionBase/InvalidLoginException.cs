using System.Net;

namespace BarberShop.Exception.ExceptionBase;

public class InvalidLoginException : BarberShopException
{
    public InvalidLoginException(string message) : base(message)
    {

    }

    public override int StatusCode => (int)HttpStatusCode.Unauthorized;

    public override List<string> GetErrors()
    {
        return [Message];
    }
}
