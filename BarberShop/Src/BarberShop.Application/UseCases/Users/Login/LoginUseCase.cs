using BarberShop.Communication.Request.User;
using BarberShop.Domain.Repositors.User;
using BarberShop.Domain.Security.Cryptography;
using BarberShop.Domain.Security.Tokens;

namespace BarberShop.Application.UseCases.Users.Login;

public class LoginUseCase : IDoLoginUseCase
{
    private readonly IUserReadOnlyRepository _userROrepository;
    private readonly ISenhaEncriptador _senhaEncriptador;
    private readonly IAccessTokenGenarator _tokenGenarator;

    public LoginUseCase(IAccessTokenGenarator tokenGenarator,
        IUserReadOnlyRepository userReadOnlyRepository,
        ISenhaEncriptador senhaEncriptador)
    {
            _senhaEncriptador = senhaEncriptador;
            _tokenGenarator = tokenGenarator;
            _userROrepository = userReadOnlyRepository;
    }

    public async Task<ResponseRegisteredUserJson> Execute(RequestLoginJson request)
    {
        var ususario = await _userROrepository.GetUserLogin(request.Email);
        if (ususario is null)
        {
            throw new InvalidLoginException(ResourceErrorMsg.INVALID_LOGIN);
        }

        var senhaCerta = _senhaEncriptador.Verify(request.Senha, ususario.Password);
        if (senhaCerta == false)
        {
            throw new InvalidLoginException(ResourceErrorMsg.INVALID_LOGIN);
        }

        return new ResponseRegisteredUserJson
        {
            Nome = ususario.Nome,
            Token = _tokenGenarator.Generate(ususario)
        };
    }
}
