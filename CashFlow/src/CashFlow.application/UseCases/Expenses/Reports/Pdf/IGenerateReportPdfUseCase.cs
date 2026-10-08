namespace CashFlow.application.UseCases.Expenses.Reports.Pdf;

public interface IGenerateReportPdfUseCase
{
    Task<byte[]> Execute(DateOnly month);
}
