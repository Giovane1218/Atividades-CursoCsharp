using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using Document = MigraDoc.DocumentObjectModel.Document;
using Font = MigraDoc.DocumentObjectModel.Font;

namespace BarberShop.Application.UseCases.Faturas.Reports.Pdf;

public class GenerateReportPdfUseCase : IGenerateReportPdfUseCase
{
    private const string CurrencySymbol = "R$";
    private readonly IFaturaReadOnlyRepository _repository;

    public GenerateReportPdfUseCase(IFaturaReadOnlyRepository repository)
    {
        _repository = repository;
    }
    public async Task<byte[]> Execute(DateOnly month)
    { 
       var faturas = await _repository.FilterByMonth(month);
        if (faturas is null)
        {
            return [];
        }

        var document = CreateDocument(month);
        var section = CreatePage(document);

        var total = SumFaturaTotal(faturas);

        CreateHeader(section);
        CreateTitle(section, month, total);

        foreach (var fatura in faturas)
        {
           var table = CreateFaturaTable(section);
            var row = table.AddRow();
            row.Height = "25";

            AddFaturaTitle(row.Cells[0], fatura.servico);

            AddHeaderAmount(row.Cells[3]);

            row = table.AddRow();
            row.Height = "25";

            row.Cells[0].AddParagraph(fatura.Data.ToString("D"));
            SetStyleBaseFaturaInfo(row.Cells[0]);

            row.Cells[1].AddParagraph(fatura.Data.ToString("t"));
            SetStyleBaseFaturaInfo(row.Cells[1]);

            row.Cells[2].AddParagraph(fatura.forma_Pgmt.PaymentTypeToString());
            SetStyleBaseFaturaInfo(row.Cells[2]);

            AddFaturaAmount(row.Cells[3], fatura.Valor);

            row = table.AddRow();
            row.Height = "25";
            

            if(string.IsNullOrWhiteSpace(fatura.nota) == false)
            {
                row.Cells[0].AddParagraph(fatura.nota);
                row.Cells[0].MergeRight = 1;
                row.Cells[0].Format.LeftIndent = "20";
            }
            SetStyleDescriptionRow(row.Cells[0]);
            SetStyleDescriptionRow(row.Cells[1]);

            row.Cells[2].AddParagraph(fatura.status.StatusToString());
            SetStyleDescriptionRow(row.Cells[2]);

            AddWhiteSpace(table);

            
        }

        return RenderDocument(document);
    }

    private Document CreateDocument(DateOnly month)
    {
        var document = new Document();
        document.Info.Title = $"{ResourceReportGenerateMsg.BILLS_FOR} {month:Y}";
        document.Info.Author = "Giba";

        var style = document.Styles["Normal"];
        style!.Font = new Font { Name = "StarJedi-Regular", Size = 12 };

        return document;
    }

    private Section CreatePage(Document document)
    {
        var section = document.AddSection();
        section.PageSetup = document.DefaultPageSetup.Clone();
        section.PageSetup.PageFormat = PageFormat.A4;
        section.PageSetup.Orientation = Orientation.Portrait;
        section.PageSetup.TopMargin = Unit.FromCentimeter(2);
        section.PageSetup.BottomMargin = Unit.FromCentimeter(2);
        section.PageSetup.LeftMargin = Unit.FromCentimeter(2);
        section.PageSetup.RightMargin = Unit.FromCentimeter(2);
        

        return section;
    }


    private void CreateHeader(Section section)
    {
        var header = section.AddParagraph();
        var headerText = string.Format(ResourceReportGenerateMsg.REPORT_HEADER);
        header.AddFormattedText(headerText, new Font{ Name = "Minecrafter", Size = 20 });
        header.Format.Alignment = ParagraphAlignment.Center;
        header.Format.SpaceAfter = "40";
    }

    private void CreateTitle(Section section, DateOnly month, decimal total)
    {
        var paragraph = section.AddParagraph();
        paragraph.Format.SpaceAfter = "40";

        var title = string.Format(ResourceReportGenerateMsg.TOTAL_BILL, month.ToString("Y"));
        paragraph.AddFormattedText(title, new Font{ Name = "StarWars", Size = 12 });

        paragraph.AddLineBreak();

        paragraph.AddFormattedText($"{CurrencySymbol} {total:F2}", new Font{ Name = "Roboto", Size = 40 });

    }

    private Table CreateFaturaTable(Section section)
    {
        var table = section.AddTable();
        table.AddColumn("215").Format.Alignment = ParagraphAlignment.Left;
        table.AddColumn("88").Format.Alignment = ParagraphAlignment.Center;
        table.AddColumn("110").Format.Alignment = ParagraphAlignment.Center;
        table.AddColumn("110").Format.Alignment = ParagraphAlignment.Right;

        return table;
    }


    private void AddFaturaTitle(Cell cell, string service)
    {
        cell.AddParagraph(service);
        cell.Shading.Color = ColorHelper.GREEN_DARK;
        cell.Format.Font = new Font { Name = "StarJedi-Regular", Size = 14, Color = ColorHelper.WHITE };
        cell.VerticalAlignment = VerticalAlignment.Center;
        cell.MergeRight = 2;
        cell.Format.LeftIndent = "20";
    }

    private decimal SumFaturaTotal(List<Fatura>faturas)
    {
        decimal valorTotal = 0;
        foreach (var fatura in faturas)
        {
            if (fatura.status == 0)
            {
                valorTotal += fatura.Valor;
            }
        }
        return valorTotal;
    }

    private void AddHeaderAmount(Cell cell)
    {
        cell.AddParagraph(ResourceReportGenerateMsg.AMOUNT);
        cell.Shading.Color = ColorHelper.GREEN_LIGHT;
        cell.Format.Font = new Font { Name = "StarJedi-Regular", Size = 14, Color = ColorHelper.WHITE };
        cell.VerticalAlignment = VerticalAlignment.Center;
    }


    private void SetStyleDescriptionRow(Cell cell)
    {
        cell.Format.Font = new Font { Name = "Roboto", Size = 10, Color = ColorHelper.BLACK };
        cell.Shading.Color = ColorHelper.GRAY_LIGHT;
        cell.VerticalAlignment = VerticalAlignment.Center;
    }
    private void SetStyleBaseFaturaInfo(Cell cell)
    {
        cell.Format.Font = new Font { Name = "Roboto", Size = 12, Color = ColorHelper.BLACK };
        cell.Shading.Color = ColorHelper.BLUE_LIGHT;
        cell.VerticalAlignment = VerticalAlignment.Center;
    }

    private void AddFaturaAmount(Cell cell, decimal valor)
    {
        cell.AddParagraph($"{CurrencySymbol} {valor}");
        cell.Format.Font = new Font { Name = "Roboto", Size = 12, Color = ColorHelper.BLACK };
        cell.VerticalAlignment = VerticalAlignment.Center;
        cell.MergeDown = 1;
    }

    private void AddWhiteSpace(Table table)
    {
        var row = table.AddRow();
        row.Height = "20";
        row.Borders.Visible = false;
    }
    private byte[] RenderDocument(Document document)
    {
        var renderer = new PdfDocumentRenderer
        {
            Document = document
        };

        renderer.RenderDocument();

        using var file = new MemoryStream();
        renderer.PdfDocument.Save(file);

        return file.ToArray();
    }
}
