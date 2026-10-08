using CashFlow.communication.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace CashFlow.application.UseCases.Expenses.Update;

public interface IUpdateExpenseUseCase
{
    Task Execute(long id, RequestExpensesJson request);
}
