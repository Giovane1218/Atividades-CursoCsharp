using AutoMapper;
using BarberShop.Domain.Services.LoggedUser;

namespace BarberShop.Application.UseCases.Faturas.GetById;

public class GetFaturaById : IGetFaturaById
{
    private readonly IFaturaReadOnlyRepository _faturaRepository;
    private readonly IMapper _mapper;
    private readonly ILoggedUser _loggedUser;
    public GetFaturaById(IFaturaReadOnlyRepository faturaRepository, IMapper mapper, ILoggedUser loggedUser)
    {
        _faturaRepository = faturaRepository;
        _mapper = mapper;
        _loggedUser = loggedUser;
    }

    public async Task<ResponseFaturaJson> Execute(long id)
    {
        var loggedUser = await _loggedUser.Get();
        var result = await _faturaRepository.GetById(id, loggedUser.Id);

        if (result is null || result.UserId != loggedUser.Id)
        {
            throw new NotFoundExeption(ResourceErrorMsg.NOT_FOUND_ERROR);
        }
        return _mapper.Map<ResponseFaturaJson>(result);
    }
}
