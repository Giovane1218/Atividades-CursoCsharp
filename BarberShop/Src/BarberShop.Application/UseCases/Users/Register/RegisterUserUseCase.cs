using BarberShop.Application.UseCases.Users.Validators;
using BarberShop.Communication.Request.User;
using BarberShop.Domain.Repositors.User;
using BarberShop.Domain.Security.Cryptography;
using BarberShop.Domain.Security.Tokens;
using FluentValidation.Results;

namespace BarberShop.Application.UseCases.Users.Register;

public class RegisterUserUseCase : IRegisterUserUseCase
{
    private readonly IMapper _mapper;
    private readonly ISenhaEncriptador _senhaEcriptador;
    private readonly IUserReadOnlyRepository _userROrepository;
    private readonly IUserWriteOnlyRepository _userWOrepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAccessTokenGenarator _tokenGanerator;
    public RegisterUserUseCase(IMapper mapper,
        ISenhaEncriptador senhaEncriptador,
        IUserReadOnlyRepository userROrepository,
        IUserWriteOnlyRepository userWOrepository,
        IAccessTokenGenarator tokenGanerator,
        IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _tokenGanerator = tokenGanerator;
        _senhaEcriptador= senhaEncriptador;
        _userROrepository = userROrepository;
        _userWOrepository = userWOrepository;
        _unitOfWork = unitOfWork;

    }
    public async Task<ResponseRegisteredUserJson> Execute(RequestUserJson request)
    {
        await Validate(request);

        var user = _mapper.Map<Usuario>(request);
        user.Password = _senhaEcriptador.Encrypt(request.Senha);
        user.UserIdentifier = Guid.NewGuid();

        user.CreateOn = DateTime.UtcNow;
        user.LastUpdateOn = null;


        await _userWOrepository.Add(user);
        await _unitOfWork.Commit();

        return new ResponseRegisteredUserJson 
        { 
            Nome = user.Nome,
            Token = _tokenGanerator.Generate(user)
        };

    }


    private async Task Validate(RequestUserJson request)
    {
        var result = new UserValidator().Validate(request);

        var emailExiste= await _userROrepository.ExisteUserComEmail(request.Email);

        if (emailExiste)
        {
            result.Errors.Add(new ValidationFailure(string.Empty, ResourceErrorMsg.EXIST_EMAIL));
        }

        if (result.IsValid == false)
        {
            var errorMsg = result.Errors.Select(f => f.ErrorMessage).ToList();

            throw new ErrorOnValidationExeption(errorMsg);
        }
    }
}
