using BarberShop.Communication.Requests;
using BarberShop.Exception;
using FluentValidation;

namespace BarberShop.Application.UseCases.Faturas;

public class FaturaValidator : AbstractValidator<RequestFaturasJson>
{
    public FaturaValidator()
    {
        RuleFor(fatura => fatura.servico).NotEmpty().WithMessage(ResourceErrorMsg.EMPTY_TITLE_ERROR);
        RuleFor(fatura => fatura.nomeCliente).NotEmpty().WithMessage(ResourceErrorMsg.EMPTY_CLIENT_NAME_ERROR);
        RuleFor(fatura => fatura.nomeBarbeiro).NotEmpty().WithMessage(ResourceErrorMsg.EMPTY_BARBER_NAME_ERROR);
        RuleFor(fatura => fatura.Valor).GreaterThanOrEqualTo(0).WithMessage(ResourceErrorMsg.VALOR_ZERO_ERROR);
        RuleFor(fatura => fatura.Data).LessThanOrEqualTo(DateTime.UtcNow).WithMessage(ResourceErrorMsg.FUTURE_DATE_ERROR);
        RuleFor(fatura => fatura.tipoPagamento).IsInEnum().WithMessage(ResourceErrorMsg.PAY_TYPE_ERROR);
        RuleFor(fatura => fatura.status).IsInEnum().WithMessage(ResourceErrorMsg.INVALID_STATUS_ERROR);
        
    }
}
