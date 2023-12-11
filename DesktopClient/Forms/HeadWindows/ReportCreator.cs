using DesktopClient.Entity;
using DesktopClient.RequestingServices;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using Path = System.IO.Path;
using TabAlignment = iText.Layout.Properties.TabAlignment;

namespace DesktopClient.Forms.HeadWindows;

static internal class ReportCreator
{
    private static readonly OperationsRequestingService OperationService = new OperationsRequestingService();

    private static Paragraph GetCenteredParagraph(string text, PdfDocument pdfDoc, Document doc)
    {
        PageSize? pageSize = pdfDoc.GetDefaultPageSize();
        float width = pageSize.GetWidth() - doc.GetLeftMargin() - doc.GetRightMargin();

        List<TabStop> tabStops = new List<TabStop>
        {
            new TabStop(width / 2, TabAlignment.CENTER)
        };

        Paragraph? output = new Paragraph().AddTabStops(tabStops);

        output.Add(new Tab())
              .Add(text);

        return output;
    }

    public static async Task CreateIncomsAndOutcomsReport(string path, DateTime from, DateTime to)
    {
        path = Path.Combine(path, $"Отчёт о расходах и доходах на период {from:dd.MM.yyyy} по {to:dd.MM.yyyy}.pdf");
        PdfDocument pdfDoc = new PdfDocument(new PdfWriter(path));

        Document doc = new Document(pdfDoc);

        PdfFont font = PdfFontFactory.CreateFont("C:\\Windows\\Fonts\\arial.ttf", "Identity-H");
        doc.SetFont(font);

        doc.Add(GetCenteredParagraph($"Отчёт о расходах и доходах на период {from:dd.MM.yyyy} по {to:dd.MM.yyyy}", pdfDoc, doc));

        doc.Add(GetCenteredParagraph(string.Empty, pdfDoc, doc));
        doc.Add(GetCenteredParagraph(string.Empty, pdfDoc, doc));
        doc.Add(GetCenteredParagraph(string.Empty, pdfDoc, doc));
        doc.Add(GetCenteredParagraph("Таблица доходов", pdfDoc, doc));

        IEnumerable<Operation> allOperations = (await OperationService.GetAllAsync()).Where(x => x.Date >= from && x.Date <= to);

        ILookup<string, decimal> incomsSumsByCategories = allOperations.Where(x => x.Category.Type.Name == "Доходы")
                                                                       .ToLookup(x => x.Category.Name, x => x.Sum);

        Table incomsTable = new Table(UnitValue.CreatePercentArray(2)).UseAllAvailableWidth();

        incomsTable.AddCell("Категория");
        incomsTable.AddCell("Сумма (BYN)");

        foreach (IGrouping<string, decimal> group in incomsSumsByCategories)
        {
            incomsTable.AddCell(group.Key);
            incomsTable.AddCell(Math.Round(group.Sum(), 2).ToString());
        }

        doc.Add(incomsTable);

        doc.Add(GetCenteredParagraph(string.Empty, pdfDoc, doc));
        doc.Add(GetCenteredParagraph(string.Empty, pdfDoc, doc));
        doc.Add(GetCenteredParagraph(string.Empty, pdfDoc, doc));
        doc.Add(GetCenteredParagraph("Таблица расходов", pdfDoc, doc));

        ILookup<string, decimal> outcomsSumsByCategories = allOperations.Where(x => x.Category.Type.Name == "Расходы")
                                                                        .ToLookup(x => x.Category.Name, x => x.Sum);

        Table outcomsTable = new Table(UnitValue.CreatePercentArray(2)).UseAllAvailableWidth();

        outcomsTable.AddCell("Категория");
        outcomsTable.AddCell("Сумма (BYN)");

        foreach (IGrouping<string, decimal> group in outcomsSumsByCategories)
        {
            outcomsTable.AddCell(group.Key);
            outcomsTable.AddCell(Math.Round(group.Sum(), 2).ToString());
        }

        doc.Add(outcomsTable);

        doc.Close();
    }

    public static async Task CreateIncomsReport(string path, DateTime from, DateTime to)
    {
        path = Path.Combine(path, $"Отчёт о доходах на период {from:dd.MM.yyyy} по {to:dd.MM.yyyy}.pdf");
        PdfDocument pdfDoc = new PdfDocument(new PdfWriter(path));

        Document doc = new Document(pdfDoc);

        PdfFont font = PdfFontFactory.CreateFont("C:\\Windows\\Fonts\\arial.ttf", "Identity-H");
        doc.SetFont(font);

        doc.Add(GetCenteredParagraph($"Отчёт о доходах на период {from:dd.MM.yyyy} по {to:dd.MM.yyyy}", pdfDoc, doc));

        doc.Add(GetCenteredParagraph(string.Empty, pdfDoc, doc));
        doc.Add(GetCenteredParagraph(string.Empty, pdfDoc, doc));
        doc.Add(GetCenteredParagraph(string.Empty, pdfDoc, doc));
        doc.Add(GetCenteredParagraph("Таблица доходов", pdfDoc, doc));

        IEnumerable<Operation> allOperations = (await OperationService.GetAllAsync()).Where(x => x.Date >= from && x.Date <= to);

        ILookup<string, decimal> incomsSumsByCategories = allOperations.Where(x => x.Category.Type.Name == "Доходы")
                                                                       .ToLookup(x => x.Category.Name, x => x.Sum);

        Table incomsTable = new Table(UnitValue.CreatePercentArray(2)).UseAllAvailableWidth();

        incomsTable.AddCell("Категория");
        incomsTable.AddCell("Сумма (BYN)");

        foreach (IGrouping<string, decimal> group in incomsSumsByCategories)
        {
            incomsTable.AddCell(group.Key);
            incomsTable.AddCell(Math.Round(group.Sum(), 2).ToString());
        }

        doc.Add(incomsTable);

        doc.Close();
    }

    public static async Task CreateOutcomsReport(string path, DateTime from, DateTime to)
    {
        path = Path.Combine(path, $"Отчёт о расходах на период {from:dd.MM.yyyy} по {to:dd.MM.yyyy}.pdf");
        PdfDocument pdfDoc = new PdfDocument(new PdfWriter(path));

        Document doc = new Document(pdfDoc);

        PdfFont font = PdfFontFactory.CreateFont("C:\\Windows\\Fonts\\arial.ttf", "Identity-H");
        doc.SetFont(font);

        doc.Add(GetCenteredParagraph($"Отчёт о расходах на период {from:dd.MM.yyyy} по {to:dd.MM.yyyy}", pdfDoc, doc));

        doc.Add(GetCenteredParagraph(string.Empty, pdfDoc, doc));
        doc.Add(GetCenteredParagraph(string.Empty, pdfDoc, doc));
        doc.Add(GetCenteredParagraph(string.Empty, pdfDoc, doc));
        doc.Add(GetCenteredParagraph("Таблица расходов", pdfDoc, doc));

        IEnumerable<Operation> allOperations = (await OperationService.GetAllAsync()).Where(x => x.Date >= from && x.Date <= to);

        ILookup<string, decimal> outcomsSumsByCategories = allOperations.Where(x => x.Category.Type.Name == "Расходы")
                                                                        .ToLookup(x => x.Category.Name, x => x.Sum);

        Table incomsTable = new Table(UnitValue.CreatePercentArray(2)).UseAllAvailableWidth();

        incomsTable.AddCell("Категория");
        incomsTable.AddCell("Сумма (BYN)");

        foreach (IGrouping<string, decimal> group in outcomsSumsByCategories)
        {
            incomsTable.AddCell(group.Key);
            incomsTable.AddCell(Math.Round(group.Sum(), 2).ToString());
        }

        doc.Add(incomsTable);

        doc.Close();
    }

    public static async Task CreateFinancialActivityReport(string path, DateTime from, DateTime to)
    {
        path = Path.Combine(path, $"Отчёт о финансовой активности отделов на период {from:dd.MM.yyyy} по {to:dd.MM.yyyy}.pdf");
        PdfDocument pdfDoc = new PdfDocument(new PdfWriter(path));

        Document doc = new Document(pdfDoc);

        PdfFont font = PdfFontFactory.CreateFont("C:\\Windows\\Fonts\\arial.ttf", "Identity-H");
        doc.SetFont(font);

        doc.Add
            (GetCenteredParagraph
                ($"Отчёт о финансовой активности отделов на период {from:dd.MM.yyyy} по {to:dd.MM.yyyy}", pdfDoc, doc));

        doc.Add(GetCenteredParagraph(string.Empty, pdfDoc, doc));
        doc.Add(GetCenteredParagraph(string.Empty, pdfDoc, doc));
        doc.Add(GetCenteredParagraph(string.Empty, pdfDoc, doc));
        doc.Add(GetCenteredParagraph("Таблица активности", pdfDoc, doc));

        IEnumerable<Operation> allOperations = (await OperationService.GetAllAsync()).Where(x => x.Date >= from && x.Date <= to);

        ILookup<string, Operation> activity = allOperations.ToLookup(x => x.Department.Name);

        Table table = new Table(UnitValue.CreatePercentArray(2)).UseAllAvailableWidth();

        table.AddCell("Название отдела");
        table.AddCell("Количество операций");

        foreach (IGrouping<string, Operation> group in activity)
        {
            table.AddCell(group.Key);
            table.AddCell(group.Count().ToString());
        }

        doc.Add(table);

        doc.Close();
    }
}