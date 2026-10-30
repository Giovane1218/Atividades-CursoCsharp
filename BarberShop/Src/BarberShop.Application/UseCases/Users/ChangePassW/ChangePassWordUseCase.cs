using BarberShop.Application.UseCases.Users.Validators;
using BarberShop.Communication.Request.User;
using BarberShop.Domain.Repositors.User;
using BarberShop.Domain.Security.Cryptography;
using BarberShop.Domain.Services.LoggedUser;
using FluentValidation.Results;

namespace BarberShop.Application.UseCases.Users.ChangePassW;

public class ChangePassWordUseCase : IChangePassWordUseCase
{
    private readonly ILoggedUser _loggedUser;
    private readonly IUserUpdateOnlyRepository _userUOrepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISenhaEncriptador _senhaEcript;

    public ChangePassWordUseCase(
        ILoggedUser loggedUser, 
        IUnitOfWork unitOfWork,
        IUserUpdateOnlyRepository userUpdateOnlyRepository,
        ISenhaEncriptador senhaEncriptador)
    {
        _loggedUser = loggedUser;
        _senhaEcript = senhaEncriptador;
        _userUOrepository = userUpdateOnlyRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task Execute(RequestChangePassWordJson request)
    {
        var loggedUser = await _loggedUser.Get();

        Validate(request, loggedUser);

        var user = await _userUOrepository.GetUserById(loggedUser.Id);
        user!.Password = _senhaEcript.Encrypt(request.NewPassword);

        _userUOrepository.Update(user);

        await _unitOfWork.Commit();
    }

    private void Validate(RequestChangePassWordJson request, Usuario logguedUser)
    {
        var validator = new ChangePassWValidator();

        var result = validator.Validate(request);

        var passWMacth = _senhaEcript.Verify(request.Password, logguedUser.Password);
        if (passWMacth == false)
        {
            result.Errors.Add(new ValidationFailure(string.Empty, ResourceErrorMsg.PASSW_DIFF_CURRENT_PASSW_ERROR));
        }

        if (result.IsValid == false)
        {
            var errors = result.Errors.Select(e => e.ErrorMessage).ToList();
            throw new ErrorOnValidationExeption(errors);
        }

    }
}
