using BarberShop.Communication.Response.Users;
using BarberShop.Domain.Repositors.User;
using BarberShop.Domain.Services.LoggedUser;

namespace BarberShop.Application.UseCases.Users.GetById;

public class GetUserUseCase : IGetUserUseCase
{
    private readonly IUserReadOnlyRepository _userROrepository;
    private readonly IMapper _mapper;
    private readonly ILoggedUser _loggedUser;

    public GetUserUseCase(IUserReadOnlyRepository userRepository, IMapper mapper, ILoggedUser loggedIUSer)
    {
        _userROrepository = userRepository;
        _mapper = mapper;
        _loggedUser = loggedIUSer;
    }

    public async Task<ResponseUserJson> Execute()
    {
        var loggedUser = await _loggedUser.Get();
        var result = await _userROrepository.GetUserById(loggedUser.Id);

        if (result is null)
        {
            throw new NotFoundExeption(ResourceErrorMsg.NOT_FOUND_ERROR);
        }
        return _mapper.Map<ResponseUserJson>(result);
    }
}
