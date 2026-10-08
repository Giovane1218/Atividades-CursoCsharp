using System;
using System.Collections.Generic;
using System.Text;

namespace CashFlow.application.UseCases.Expenses.Reports.Excel;

public interface IGenerateReportExcelUseCase
{
    public Task<byte[]> Execute(DateOnly month);
}
