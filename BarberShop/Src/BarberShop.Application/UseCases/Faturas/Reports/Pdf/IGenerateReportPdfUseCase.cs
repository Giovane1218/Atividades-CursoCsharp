namespace BarberShop.Application.UseCases.Faturas.Reports.Pdf;

public interface IGenerateReportPdfUseCase
{
    Task<byte[]> Execute(DateOnly month);
}
