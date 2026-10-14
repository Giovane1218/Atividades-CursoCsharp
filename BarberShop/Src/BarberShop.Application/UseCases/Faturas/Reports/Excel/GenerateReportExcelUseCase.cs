using ClosedXML.Excel;
using BarberShop.Domain.Reports;
using BarberShop.Domain.Repositors.Faturas;
using BarberShop.Domain.Enums.Extensions;

namespace BarberShop.Application.UseCases.Faturas.Reports.Excel;

public class GenerateReportExcelUseCase : IGenerateReportExcelUseCase
{                  
    private const string Moeda = "R$";
    private readonly IFaturaReadOnlyRepository _repository;
    public GenerateReportExcelUseCase(IFaturaReadOnlyRepository repository)
    {
        _repository = repository;
    }

    public async Task<byte[]> Execute(DateOnly month)
    {
        var faturas = await _repository.FilterByMonth(month);
        if (faturas.Count == 0)
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
        foreach (var fatura in faturas)
        {
            workSheet.Cell($"A{row}").Value = fatura.servico;
            workSheet.Cell($"B{row}").Value = fatura.nome_Barbeiro;
            workSheet.Cell($"C{row}").Value = fatura.nome_Cliente;
            workSheet.Cell($"D{row}").Value = fatura.Data.ToString("dd/MM/yyyy");
            workSheet.Cell($"E{row}").Value = $"{Moeda} {fatura.Valor:#,##.00}";
            workSheet.Cell($"F{row}").Value = fatura.forma_Pgmt.PaymentTypeToString();
            workSheet.Cell($"G{row}").Value = fatura.status.StatusToString();
            workSheet.Cell($"H{row}").Value = fatura.nota;


            row++;
        }

        workSheet.Columns().AdjustToContents();

        var file = new MemoryStream();
        workBook.SaveAs(file);
        return file.ToArray();
    }

    private void Insertheader(IXLWorksheet workSheet)
    {
        workSheet.Cell("A1").Value = ResourceReportGenerateMsg.SERVICE;
        workSheet.Cell("B1").Value = ResourceReportGenerateMsg.BARBER_NAME;
        workSheet.Cell("C1").Value = ResourceReportGenerateMsg.CLIENT_NAME;
        workSheet.Cell("D1").Value = ResourceReportGenerateMsg.DATE;
        workSheet.Cell("E1").Value = ResourceReportGenerateMsg.AMOUNT;
        workSheet.Cell("F1").Value = ResourceReportGenerateMsg.PAYMENT_TYPE;
        workSheet.Cell("G1").Value = ResourceReportGenerateMsg.STATUS;
        workSheet.Cell("H1").Value = ResourceReportGenerateMsg.DESCRIPTION;

        workSheet.Cells("A1:H1").Style.Font.Bold = true;
        workSheet.Cells("A1:H1").Style.Fill.BackgroundColor = XLColor.Green;

        workSheet.Cell("A1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        workSheet.Cell("B1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        workSheet.Cell("C1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        workSheet.Column("D").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        workSheet.Column("E").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right);
        workSheet.Cell("E1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        workSheet.Cell("F1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        workSheet.Cell("G1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        workSheet.Cell("H1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
    }
}
