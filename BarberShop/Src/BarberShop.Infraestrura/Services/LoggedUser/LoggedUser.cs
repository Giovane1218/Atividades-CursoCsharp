using BarberShop.Domain.Entidades;
using BarberShop.Domain.Services.LoggedUser;
using BarberShop.Infraestrura.Security.Token;
using BarberShop.Infraestrutura.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BarberShop.Infraestrura.Services.LoggedUser;

public class LoggedUser : ILoggedUser
{
    private readonly BarberShopDBContext _dbContext;
    private readonly ITokenProvider _tokenProvider;

    public LoggedUser(BarberShopDBContext dBContext, ITokenProvider tokenProvider)
    {
        _tokenProvider = tokenProvider;
        _dbContext = dBContext;
    }
    public async Task<Usuario> Get()
    {
        string token = _tokenProvider.TokenOnRequest();

        var tokenHandler = new JwtSecurityTokenHandler();
        var Securitytoken = tokenHandler.ReadJwtToken(token);
        var identifier = Securitytoken.Claims.First(claim => claim.Type == ClaimTypes.Sid).Value;

        return await _dbContext
            .Usuario.AsNoTracking()
            .FirstAsync(user => user.UserIdentifier == Guid.Parse(identifier));
    }
}
