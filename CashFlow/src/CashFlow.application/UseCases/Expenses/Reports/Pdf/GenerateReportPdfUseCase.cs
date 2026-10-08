using CashFlow.Domain.Enums.Extensions;
using CashFlow.Domain.Reports;
using CashFlow.Domain.Repositors.Expenses;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using Document = MigraDoc.DocumentObjectModel.Document;
using Font = MigraDoc.DocumentObjectModel.Font;

namespace CashFlow.application.UseCases.Expenses.Reports.Pdf;

public class GenerateReportPdfUseCase : IGenerateReportPdfUseCase
{
    private const string CurrencySymbol = "R$";
    private readonly IExpenseReadOnlyRepository _repository;

    public GenerateReportPdfUseCase(IExpenseReadOnlyRepository repository)
    {
        _repository = repository;
    }
    public async Task<byte[]> Execute(DateOnly month)
    { 
       var expenses = await _repository.FilterByMonth(month);
        if (expenses is null)
        {
            return [];
        }

        var document = CreateDocument(month);
        var section = CreatePage(document);
        
        var total = expenses.Sum(expense => expense.Valor);

        CreateHeader(section);
        CreateTitle(section, month, total);

        foreach (var expense in expenses)
        {
           var table = CreateExpenseTable(section);
            var row = table.AddRow();
            row.Height = "25";

            AddExpensetitle(row.Cells[0], expense.Titulo);

            AddHeaderAmount(row.Cells[3]);

            row = table.AddRow();
            row.Height = "25";

            row.Cells[0].AddParagraph(expense.Data.ToString("D"));
            SetStyleBaseExpenseInfo(row.Cells[0]);

            row.Cells[1].AddParagraph(expense.Data.ToString("t"));
            SetStyleBaseExpenseInfo(row.Cells[1]);

            row.Cells[2].AddParagraph(expense.tipoPagamento.PaymentTypeToString());
            SetStyleBaseExpenseInfo(row.Cells[2]);

            AddExpenseAmount(row.Cells[3], expense.Valor);

            if(string.IsNullOrWhiteSpace(expense.Descricao) == false)
            {
                var descriptionRow = table.AddRow();
                descriptionRow.Height = "25";

                descriptionRow.Cells[0].AddParagraph(expense.Descricao);
                descriptionRow.Cells[0].Format.Font = new Font { Name = "Roboto", Size = 10, Color = ColorHelper.BLACK };
                descriptionRow.Cells[0].Shading.Color = ColorHelper.GREEN_LIGHT;
                descriptionRow.Cells[0].VerticalAlignment = VerticalAlignment.Center;
                descriptionRow.Cells[0].MergeRight = 2;
                descriptionRow.Cells[0].Format.LeftIndent = "20";

                row.Cells[3].MergeDown = 1;
            }

            AddWhiteSpace(table);

            
        }

        return RenderDocument(document);
    }

    private Document CreateDocument(DateOnly month)
    {
        var document = new Document();
        document.Info.Title = $"{ResourceReportGenerateMsg.EXPENSE_FOR} {month:Y}";
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
        var header = section.Headers.Primary.AddParagraph();
        var headerText = string.Format(ResourceReportGenerateMsg.REPORT_HEADER);
        header.AddFormattedText(headerText, new Font{ Name = "Minecrafter", Size = 20 });
        header.Format.Alignment = ParagraphAlignment.Center;
        header.Format.SpaceAfter = "40";
    }

    private void CreateTitle(Section section, DateOnly month, decimal total)
    {
        var paragraph = section.AddParagraph();
        paragraph.Format.SpaceAfter = "40";

        var title = string.Format(ResourceReportGenerateMsg.TOTAL_SPENT, month.ToString("Y"));
        paragraph.AddFormattedText(title, new Font{ Name = "StarWars", Size = 12 });

        paragraph.AddLineBreak();

        paragraph.AddFormattedText($"{CurrencySymbol} {total:F2}", new Font{ Name = "Roboto", Size = 40 });

    }

    private Table CreateExpenseTable(Section section)
    {
        var table = section.AddTable();
        table.AddColumn("195").Format.Alignment = ParagraphAlignment.Left;
        table.AddColumn("88").Format.Alignment = ParagraphAlignment.Center;
        table.AddColumn("120").Format.Alignment = ParagraphAlignment.Center;
        table.AddColumn("120").Format.Alignment = ParagraphAlignment.Right;

        return table;
    }


    private void AddExpensetitle(Cell cell, string expenseTitle)
    {
        cell.AddParagraph(expenseTitle);
        cell.Shading.Color = ColorHelper.RED_LIGHT;
        cell.Format.Font = new Font { Name = "StarJedi-Regular", Size = 14, Color = ColorHelper.BLACK };
        cell.VerticalAlignment = VerticalAlignment.Center;
        cell.MergeRight = 2;
        cell.Format.LeftIndent = "20";
    }

    private void AddHeaderAmount(Cell cell)
    {
        cell.AddParagraph(ResourceReportGenerateMsg.AMOUNT);
        cell.Shading.Color = ColorHelper.RED_DARK;
        cell.Format.Font = new Font { Name = "StarJedi-Regular", Size = 14, Color = ColorHelper.WHITE };
        cell.VerticalAlignment = VerticalAlignment.Center;
    }

    private void SetStyleBaseExpenseInfo(Cell cell)
    {
        cell.Format.Font = new Font { Name = "Roboto", Size = 12, Color = ColorHelper.BLACK };
        cell.Shading.Color = ColorHelper.GREEN_DARK;
        cell.VerticalAlignment = VerticalAlignment.Center;
    }

    private void AddExpenseAmount(Cell cell, decimal valor)
    {
        cell.AddParagraph($"-{CurrencySymbol} {valor}");
        cell.Format.Font = new Font { Name = "Roboto", Size = 12, Color = ColorHelper.BLACK };
        cell.VerticalAlignment = VerticalAlignment.Center;
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
