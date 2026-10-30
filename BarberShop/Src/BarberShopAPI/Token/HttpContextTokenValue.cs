using BarberShop.Infraestrura.Security.Token;

namespace BarberShop.API.Token;

public class HttpContextTokenValue : ITokenProvider
{
    private readonly IHttpContextAccessor _contextAccessor;
    public HttpContextTokenValue(IHttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
    }
    public string TokenOnRequest()
    {
        var authorizathion = _contextAccessor.HttpContext!.Request.Headers.Authorization.ToString();

        return authorizathion["Bearer ".Length..].Trim();
    }
}
