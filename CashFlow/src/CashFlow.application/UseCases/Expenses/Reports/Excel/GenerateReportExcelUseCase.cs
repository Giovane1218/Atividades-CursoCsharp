using ClosedXML.Excel;
using CashFlow.Domain.Reports;
using CashFlow.Domain.Repositors.Expenses;
using CashFlow.Domain.Enums.Extensions;

namespace CashFlow.application.UseCases.Expenses.Reports.Excel;

public class GenerateReportExcelUseCase : IGenerateReportExcelUseCase
{                  
    private const string Moeda = "R$";
    private readonly IExpenseReadOnlyRepository _repository;
    public GenerateReportExcelUseCase(IExpenseReadOnlyRepository repository)
    {
        _repository = repository;
    }

    public async Task<byte[]> Execute(DateOnly month)
    {
        var expenses = await _repository.FilterByMonth(month);
        if (expenses.Count == 0)
        {
            return [];
        }

        using var workBook = new XLWorkbook();

        workBook.Author = "Gibinha";
        workBook.Style.Font.FontName = "Arial";
        workBook.Style.Font.FontSize = 12;
        
        var workSheet = workBook.Worksheets.Add(month.ToString("Y"));

        Insertheader(workSheet);

        var row = 2;
        foreach (var expense in expenses)
        {
            workSheet.Cell($"A{row}").Value = expense.Titulo;
            workSheet.Cell($"B{row}").Value = expense.Descricao;
            workSheet.Cell($"C{row}").Value = expense.Data.ToString("dd/MM/yyyy");
            workSheet.Cell($"D{row}").Value = $"- {Moeda} {expense.Valor:#,##.00}";
            workSheet.Cell($"E{row}").Value = expense.tipoPagamento.PaymentTypeToString();

            row++;
        }

        workSheet.Columns().AdjustToContents();

        var file = new MemoryStream();
        workBook.SaveAs(file);
        return file.ToArray();
    }

    private void Insertheader(IXLWorksheet workSheet)
    {
        workSheet.Cell("A1").Value = ResourceReportGenerateMsg.TITLE;
        workSheet.Cell("B1").Value = ResourceReportGenerateMsg.DESCRIPTION;
        workSheet.Cell("C1").Value = ResourceReportGenerateMsg.DATE;
        workSheet.Cell("D1").Value = ResourceReportGenerateMsg.AMOUNT;
        workSheet.Cell("E1").Value = ResourceReportGenerateMsg.PAYMENT_TYPE;

        workSheet.Cells("A1:E1").Style.Font.Bold = true;
        workSheet.Cells("A1:E1").Style.Fill.BackgroundColor = XLColor.Green;

        workSheet.Cell("A1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        workSheet.Cell("B1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        workSheet.Cell("C1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        workSheet.Cell("D1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right);
        workSheet.Cell("E1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
    }
}
