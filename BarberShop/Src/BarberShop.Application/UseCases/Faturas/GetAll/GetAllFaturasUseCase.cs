using BarberShop.Domain.Services.LoggedUser;

namespace BarberShop.Application.UseCases.Faturas.GetAll;

public class GetAllFaturasUseCase : IGetAllFaturasUseCase
{
    private readonly IFaturaReadOnlyRepository _FaturaRepository;
    private readonly IMapper _mapper;
    private readonly ILoggedUser _loggedUser;

    public GetAllFaturasUseCase(IFaturaReadOnlyRepository FaturaRepository, 
        IMapper mapper,
        ILoggedUser loggedUser)
    {
        _FaturaRepository = FaturaRepository;
        _mapper = mapper;
        _loggedUser = loggedUser;
    }
    public async Task<ResponseAllFaturasJson> Execute()
    {
        var loggedUser = await _loggedUser.Get();
        var result = await _FaturaRepository.GetAll(loggedUser.Id);

        return new ResponseAllFaturasJson
        {
            Faturas = _mapper.Map<List<ResponseShortFaturaJson>>(result)
        };
    }
}
