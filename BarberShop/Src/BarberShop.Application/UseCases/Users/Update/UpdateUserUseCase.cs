using BarberShop.Application.UseCases.Users.Validators;
using BarberShop.Communication.Request.User;
using BarberShop.Domain.Repositors.User;
using BarberShop.Domain.Services.LoggedUser;
using FluentValidation.Results;
using System.Xml;

namespace BarberShop.Application.UseCases.Users.Update;

public class UpdateUserUseCase : IUpdateUserUseCase
{
    private readonly IUserReadOnlyRepository _userROrepository;
    private readonly IUserUpdateOnlyRepository _userUOrepository;
    private readonly ILoggedUser _loggedUser;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateUserUseCase(IUnitOfWork unitOfWork, 
        IUserUpdateOnlyRepository updateOnlyRepository,
        ILoggedUser loggedUser,
        IUserReadOnlyRepository userReadOnly)
    {
        _loggedUser = loggedUser;
        _unitOfWork = unitOfWork;
        _userUOrepository = updateOnlyRepository;
        _userROrepository = userReadOnly;
    }

    public async Task Execute(RequestUpdateUserJson request)
    {
        var loggedUser = await _loggedUser.Get();
        await Validate(request, loggedUser.Email);
        var user = await _userUOrepository.GetUserById(loggedUser.Id);
        if (user is null)
        {
            throw new NotFoundExeption(ResourceErrorMsg.NOT_FOUND_ERROR);
        }
        user.Nome = request.Nome;
        user.Email = request.Email;
        user.LastUpdateOn = DateTime.UtcNow;

        _userUOrepository.Update(user);

        await _unitOfWork.Commit();
    }

    private async Task Validate(RequestUpdateUserJson request, string emailAtual)
    {
        var result = new UpdateUserValidator().Validate(request);


        if (emailAtual.Equals(request.Email) == false)
        {
            var emailExiste = await _userROrepository.ExisteUserComEmail(request.Email);
            if (emailExiste)
            {
                result.Errors.Add(new ValidationFailure(string.Empty, ResourceErrorMsg.EXIST_EMAIL));
            }
        }

        if (result.IsValid == false)
        {
            var errorMsg = result.Errors.Select(f => f.ErrorMessage).ToList();

            throw new ErrorOnValidationExeption(errorMsg);
        }
    }


}
