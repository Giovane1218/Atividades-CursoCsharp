using BarberShop.Domain.Entidades;
using BarberShop.Domain.Security.Tokens;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BarberShop.Infraestrura.Security.Token;

public class JwtTokenGenerator : IAccessTokenGenarator
{
    private readonly string _signingKey;
    private readonly uint _expirationTimeMin;
    public JwtTokenGenerator(uint expiration_time_minutes, string signingKey)
    {
        _expirationTimeMin = expiration_time_minutes;
        _signingKey = signingKey;
    }

    public string Generate(Usuario user)
    {
        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Sid, user.UserIdentifier.ToString())
        };
        var token_descriptor = new SecurityTokenDescriptor
        {
            Expires = DateTime.UtcNow.AddMinutes(_expirationTimeMin),
            SigningCredentials = new SigningCredentials(SecurityKey(), SecurityAlgorithms.HmacSha256Signature),
            Subject = new ClaimsIdentity(claims)
        };

        var token_handler = new JwtSecurityTokenHandler();
        var security_token = token_handler.CreateToken(token_descriptor);

        return token_handler.WriteToken(security_token);
    }


    private SymmetricSecurityKey SecurityKey()
    {
        var security_key = Encoding.UTF8.GetBytes(_signingKey);

        return new SymmetricSecurityKey(security_key);
    }
}
