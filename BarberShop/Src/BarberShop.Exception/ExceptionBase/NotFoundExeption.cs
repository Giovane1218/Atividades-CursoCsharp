using System.Net;

namespace BarberShop.Exception.ExceptionBase;

public class NotFoundExeption : BarberShopException
{
    public NotFoundExeption(string message) : base(message)
    {
        
    }

    public override int StatusCode => (int)HttpStatusCode.NotFound;

    public override List<string> GetErrors()
    {
        return [Message];
    }
}
