using Bogus;
using CashFlow.communication.Enums;
using CashFlow.communication.Requests;

namespace CommunTestUtilities.Request;

public class RequestRegisterExpensesJsonBuilder
{
    public static RequestExpensesJson Build()
    {
        return new Faker<RequestExpensesJson>()
            .RuleFor(r => r.Titulo, faker => faker.Commerce.ProductName())
            .RuleFor(r => r.Valor, faker => faker.Finance.Amount(1, 1000))
            .RuleFor(r => r.Data, faker => faker.Date.Past())
            .RuleFor(r => r.Descricao, faker => faker.Commerce.ProductDescription())
            .RuleFor(r => r.PaymentType, faker => faker.PickRandom<PaymentType>());
    }
}