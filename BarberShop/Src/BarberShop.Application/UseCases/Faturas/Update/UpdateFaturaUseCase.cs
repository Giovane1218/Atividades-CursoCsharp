using AutoMapper;
using BarberShop.Communication.Requests;
using BarberShop.Domain.Entidades;
using BarberShop.Domain.Repositors;
using BarberShop.Domain.Repositors.Faturas;
using BarberShop.Exception;
using BarberShop.Exception.ExceptionBase;
using Microsoft.IdentityModel.Tokens.Experimental;
using System.ComponentModel.DataAnnotations;

namespace BarberShop.Application.UseCases.Faturas.Update;

public class UpdateFaturaUseCase : IUpdateFaturaUseCase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IFaturaUpdateOnlyRepository _repository;

    public UpdateFaturaUseCase(IUnitOfWork unitOfWork, IMapper mapper, IFaturaUpdateOnlyRepository faturaUpdateOnlyRepository)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _repository = faturaUpdateOnlyRepository;
    }

    public async Task Execute(long id, RequestFaturasJson request)
    {
        Validate(request);
        var fatura = await _repository.GetById(id);

        if (fatura is null)
        {
            throw new NotFoundExeption(ResourceErrorMsg.NOT_FOUND_ERROR);
        }

        fatura.atualizado_Em = DateTime.UtcNow;
        _mapper.Map(request, fatura);

        _repository.Update(fatura);

        await _unitOfWork.Commit();
    }

    private void Validate(RequestFaturasJson request)
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
