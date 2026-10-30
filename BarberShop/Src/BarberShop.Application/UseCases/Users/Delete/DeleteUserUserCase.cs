using BarberShop.Domain.Repositors.User;
using BarberShop.Domain.Services.LoggedUser;

namespace BarberShop.Application.UseCases.Users.Delete;

public class DeleteUserUserCase : IDeleteUserUseCase
{
    private readonly IUserWriteOnlyRepository _userWOrepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILoggedUser _loggedUser;

    public DeleteUserUserCase(
        IUnitOfWork unitOfWork,
        IUserWriteOnlyRepository userWriteOnlyRepository,
        ILoggedUser loggedUser)
    {
        _loggedUser = loggedUser;
        _unitOfWork = unitOfWork;
        _userWOrepository = userWriteOnlyRepository;
    }
    public async Task Execute()
    {
        var loggedUser = await _loggedUser.Get();
        await _userWOrepository.Delete(loggedUser);

        await _unitOfWork.Commit();
    }
}
