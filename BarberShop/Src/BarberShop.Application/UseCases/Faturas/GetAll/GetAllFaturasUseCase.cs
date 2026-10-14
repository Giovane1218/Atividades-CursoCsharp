using AutoMapper;

namespace BarberShop.Application.UseCases.Faturas.GetAll;

public class GetAllFaturasUseCase : IGetAllFaturasUseCase
{
    private readonly IFaturaReadOnlyRepository _FaturaRepository;
    private readonly IMapper _mapper;
    public GetAllFaturasUseCase(IFaturaReadOnlyRepository FaturaRepository, IMapper mapper)
    {
        _FaturaRepository = FaturaRepository;
        _mapper = mapper;
    }
    public async Task<ResponseAllFaturasJson> Execute()
    {
        var result = await _FaturaRepository.GetAll();

        return new ResponseAllFaturasJson
        {
            Faturas = _mapper.Map<List<ResponseShortFaturaJson>>(result)
        };
    }
}
