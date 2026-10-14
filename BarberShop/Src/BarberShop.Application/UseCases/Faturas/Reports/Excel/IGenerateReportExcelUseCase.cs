using System;
using System.Collections.Generic;
using System.Text;

namespace BarberShop.Application.UseCases.Faturas.Reports.Excel;

public interface IGenerateReportExcelUseCase
{
    public Task<byte[]> Execute(DateOnly month);
}
