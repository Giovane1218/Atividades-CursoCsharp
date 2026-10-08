using CashFlow.application.UseCases.Expenses;
using CashFlow.communication.Requests;
using CashFlow.Exeption;
using CommunTestUtilities.Request;
using FluentAssertions;

namespace Validators.testes.Expenses.RegisterExpenses;

public class RegisterExpenseValidatorTests
{
    [Fact]
    public void Sucesso()
    {
        //Arrange
        var validator = new ExpenseValidator();
        var request = RequestRegisterExpensesJsonBuilder.Build();

        //Act
        var result = validator.Validate(request);

        //Assert
        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Error_Titulo_Empty()
    {
        //Arrange
        var validator = new ExpenseValidator();
        var request = RequestRegisterExpensesJsonBuilder.Build();
        request.Titulo = string.Empty;
        //Act
        var result = validator.Validate(request);
        //Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMsg.EMPTY_TITLE_ERROR));
    }

    [Theory]
    [InlineData]
    public void ErrorValorZero()
    {
        //Arrange
        var validator = new ExpenseValidator();
        var request = RequestRegisterExpensesJsonBuilder.Build();
        request.Valor = 0;
        //Act
        var result = validator.Validate(request);
        //Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMsg.VALOR_ZERO_ERROR));
    }

    [Fact]
    public void Error_Data_Future()
    {
        //Arrange
        var validator = new ExpenseValidator();
        var request = RequestRegisterExpensesJsonBuilder.Build();
        request.Data = DateTime.Now.AddDays(1);
        //Act
        var result = validator.Validate(request);
        //Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMsg.FUTURE_DATE_ERROR));
    }

}
