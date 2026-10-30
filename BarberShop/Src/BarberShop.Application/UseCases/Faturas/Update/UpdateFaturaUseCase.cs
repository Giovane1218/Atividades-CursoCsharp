using BarberShop.Domain.Services.LoggedUser;

namespace BarberShop.Application.UseCases.Faturas.Update;

public class UpdateFaturaUseCase : IUpdateFaturaUseCase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IFaturaUpdateOnlyRepository _repository;
    private readonly ILoggedUser _loggedUser;

    public UpdateFaturaUseCase(IUnitOfWork unitOfWork, 
        IMapper mapper, 
        IFaturaUpdateOnlyRepository faturaUpdateOnlyRepository,
        ILoggedUser loggeduser)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _repository = faturaUpdateOnlyRepository;
        _loggedUser = loggeduser;
    }

    public async Task Execute(long id, RequestFaturasJson request)
    {
        await Validate(request);
        var loggedUser = await _loggedUser.Get();
        var fatura = await _repository.GetById(id, loggedUser.Id);
        if (fatura is null)
        {
            throw new NotFoundExeption(ResourceErrorMsg.NOT_FOUND_ERROR);
        }

        fatura.Tags.Clear();
        fatura.LastUpdateOn = DateTime.UtcNow;
        _mapper.Map(request, fatura);

        _repository.Update(fatura);

        await _unitOfWork.Commit();
    }

    private async Task Validate(RequestFaturasJson request)
    {
        var validator = new FaturaValidator();
        var result = validator.Validate(request);
        if (result.IsValid == false)
        {
            var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();
            throw new ErrorOnValidationExeption(errorMessages);
        }
    }


}
