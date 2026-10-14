namespace BarberShop.Application.UseCases.Faturas.Register;

public class RegisterFaturaUseCase : IRegisterFaturaUseCase
{
    private readonly IFaturaWriteOnlyRepository _faturaRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public RegisterFaturaUseCase(
        IFaturaWriteOnlyRepository faturaRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _faturaRepository = faturaRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ResponseRegisterFaturaJson> Execute(RequestFaturasJson request)
    {
        Validate(request);

        var fatura = _mapper.Map<Fatura>(request);
        fatura.criado_Em = DateTime.UtcNow;
        fatura.atualizado_Em = null;
        await _faturaRepository.Add(fatura);
        await _unitOfWork.Commit();

        return _mapper.Map<ResponseRegisterFaturaJson>(fatura);
    }

    private static void Validate(RequestFaturasJson request)
    {
        var validator = new FaturaValidator();
        var result = validator.Validate(request);

        if (result.IsValid == false)
        {
            var errorMessages = result.Errors.Select(error => error.ErrorMessage).ToList();
            throw new ErrorOnValidationExeption(errorMessages);
        }
    }
}
