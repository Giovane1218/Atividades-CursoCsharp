using AutoMapper;

namespace BarberShop.Application.UseCases.Faturas.GetById;

public class GetFaturaById : IGetFaturaById
{
    private readonly IFaturaReadOnlyRepository _faturaRepository;
    private readonly IMapper _mapper;
    public GetFaturaById(IFaturaReadOnlyRepository faturaRepository, IMapper mapper)
    {
        _faturaRepository = faturaRepository;
        _mapper = mapper;
    }

    public async Task<ResponseFaturaJson> Execute(long id)
    {
        var result = await _faturaRepository.GetById(id);

        if (result is null)
        {
            throw new NotFoundExeption(ResourceErrorMsg.NOT_FOUND_ERROR);
        }
        return _mapper.Map<ResponseFaturaJson>(result);
    }
}
