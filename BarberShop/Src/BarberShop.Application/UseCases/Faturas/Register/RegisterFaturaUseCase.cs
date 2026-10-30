using BarberShop.Domain.Services.LoggedUser;

namespace BarberShop.Application.UseCases.Faturas.Register;

public class RegisterFaturaUseCase : IRegisterFaturaUseCase
{
    private readonly IFaturaWriteOnlyRepository _faturaRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILoggedUser _loggedUser;

    public RegisterFaturaUseCase(
        IFaturaWriteOnlyRepository faturaRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        ILoggedUser loggedUser)
    {
        _faturaRepository = faturaRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _loggedUser = loggedUser;
    }

    public async Task<ResponseRegisterFaturaJson> Execute(RequestFaturasJson request)
    {
        Validate(request);
        var loggedUser = await _loggedUser.Get();

        var fatura = _mapper.Map<Fatura>(request);
        fatura.UserId = loggedUser.Id;
        fatura.CreateOn = DateTime.UtcNow;
        fatura.LastUpdateOn = null;

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
