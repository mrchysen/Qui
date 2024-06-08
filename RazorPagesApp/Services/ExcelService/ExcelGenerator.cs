using ClosedXML.Excel;
using DocumentFormat.OpenXml.Office2016.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using RazorPagesApp.Models;

namespace RazorPagesApp.Services.ExcelService;


/// <summary>
/// Класс для генерации ексель файлов
/// </summary>
public class ExcelGenerator : IExcelService
{
    protected int MaxQuestions;

    public ExcelGenerator() { }

    public bool CreateExcelFile(List<User> users)
    {
        try
        {
            using var workbook = new XLWorkbook();

            var worksheet = workbook.AddWorksheet("Лист1");
            
            if(users.Count > 0)
                MaxQuestions = users.Max((user) => user.Progress.Answers.Count);

            GenerateHeader(worksheet, users);
            WriteAllUser(worksheet, users);

            worksheet.Columns(1,10 + MaxQuestions * 5).AdjustToContents();

            workbook.SaveAs(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "results.xlsx"));
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }

        return true;
    }

    protected void WriteAllUser(IXLWorksheet sheet, List<User> users)
    {
        for (int i = 0; i < users.Count; i++)
        {
            AddUserInfoLine(sheet, i, users[i]);
        }

        // Add border to end
        sheet.Range(users.Count + 2, 1, users.Count + 2, 9 + 5 * MaxQuestions)
            .Style.Border.BottomBorder = XLBorderStyleValues.Thick;
        sheet.Range(users.Count + 2, 1, users.Count + 2, 9 + 5 * MaxQuestions)
            .Style.Border.BottomBorderColor = XLColor.Black;
    }

    protected void AddUserInfoLine(IXLWorksheet sheet, int i, User user)
    {
        AddCellValue(sheet, i + 3, 1, (i + 1).ToString(), true);
        AddCellValue(sheet, i + 3, 2, user.GetStartTime().ToShortDateString());
        AddCellValue(sheet, i + 3, 3, user.GetFullName());
        AddCellValue(sheet, i + 3, 4, user.Sex == Sex.Man ? "М" : "Ж");
        AddCellValue(sheet, i + 3, 5, user.Age.ToString());

        for (int j = 0; IndexCondition(j, user); j++)
        {
            AddCellValue(sheet, i + 3, 6 + j * 5, user.Progress.Answers[j]); // ответ
            AddCellValue(sheet, i + 3, 6 + j * 5 + 1, user.Progress.AnswerStartDateTime[j].ToLongTimeString() + $" {user.Progress.AnswerStartDateTime[j].Millisecond}"); // время начала
            AddCellValue(sheet, i + 3, 6 + j * 5 + 2, user.Progress.AnswerEndDateTime[j].ToLongTimeString() + $" {user.Progress.AnswerStartDateTime[j].Millisecond}");   // время нажатия на кнопку далее
            AddCellValue(sheet, i + 3, 6 + j * 5 + 3, (user.Progress.IsRightAnswerList[j] ? 1 : 0).ToString()); // правильность ответа
            AddCellValue(sheet, i + 3, 6 + j * 5 + 4, (user.Progress.WasSearched[j] ? 1 : 0).ToString()); // искал ли в инете
        }

        AddCellValue(sheet, i + 3, 6 + MaxQuestions * 5, user.Progress.RightAnswers.ToString());
        AddCellValue(sheet, i + 3, 6 + MaxQuestions * 5 + 1, user.Progress.Answers.Count.ToString());
        AddCellValue(sheet, i + 3, 6 + MaxQuestions * 5 + 2, user.Progress.WasSearched.Select(e => e ? 1 : 0).Sum().ToString());
        
        TimeSpan timeSpan = new(0, 0, 0);

        if (user.Progress.AnswerStartDateTime.Count > 0 && user.Progress.AnswerEndDateTime.Count > 0)
            timeSpan = user.Progress.AnswerEndDateTime.Last() - user.Progress.AnswerStartDateTime[0];

        AddCellValue(sheet, i + 3, 6 + MaxQuestions * 5 + 3, timeSpan.ToString());
    }

    protected bool IndexCondition(int index, User User)
    {
        return index < User.Progress.Answers.Count &&
            index < User.Progress.IsRightAnswerList.Count &&
            index < User.Progress.AnswerEndDateTime.Count &&
            index < User.Progress.AnswerStartDateTime.Count &&
            index < User.Progress.WasSearched.Count;
    }

    protected void AddCellValue(IXLWorksheet sheet, int CellRow, int CellColumn, string value, bool VerticalAlig = false)
    {
        sheet.Cell(CellRow, CellColumn).Value = value;
        sheet.Cell(CellRow, CellColumn).Style.Border.RightBorder = XLBorderStyleValues.Thin;
        sheet.Cell(CellRow, CellColumn).Style.Border.RightBorderColor = XLColor.Black;
        sheet.Cell(CellRow, CellColumn).Style.Border.BottomBorder = XLBorderStyleValues.Thick;
        sheet.Cell(CellRow, CellColumn).Style.Border.BottomBorderColor = XLColor.Black;
        if (VerticalAlig)
            sheet.Cell(CellRow, CellColumn).Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
    }

    protected void GenerateHeader(IXLWorksheet sheet, List<User> users)
    {
        SimpleHeaderPart(sheet, 1, 1, "Номер");
        SimpleHeaderPart(sheet, 2, 1, "Дата");
        SimpleHeaderPart(sheet, 3, 1, "ФИО");
        SimpleHeaderPart(sheet, 4, 1, "Пол");
        SimpleHeaderPart(sheet, 5, 1, "Возраст");

        
        for (int i = 0; i < MaxQuestions; i++)
        {
            QuestionCellGenerate(sheet, i);
        }

        int endHeaders = MaxQuestions * 5 + 6;

        SimpleHeaderPart(sheet, endHeaders, 1, "Правильных ответов", 25);
        SimpleHeaderPart(sheet, endHeaders + 1, 1, "Всего ответов", 25);
        SimpleHeaderPart(sheet, endHeaders + 2, 1, "Кол-во поисков в интернете", 25);
        SimpleHeaderPart(sheet, endHeaders + 3, 1, "Время прохождния", 25);

    }

    /// <summary>
    /// you should numberOfQuestion + 1, because numeration begins with 0
    /// </summary>
    /// <param name="sheet"></param>
    /// <param name="numberOfQuestion"></param>
    protected void QuestionCellGenerate(IXLWorksheet sheet, int numberOfQuestion)
    {
        int cell = 6 + numberOfQuestion * 5;

        sheet.Cell(1, cell).Value = "Вопрос №" + (numberOfQuestion + 1);
        for (int i = 0; i < 5; i++)
        {
            sheet.Cell(1, cell + i).Style.Border.BottomBorder = XLBorderStyleValues.Medium;
            sheet.Cell(1, cell + i).Style.Border.BottomBorderColor = XLColor.Black;
            sheet.Cell(1, cell + i).Style.Border.LeftBorder = XLBorderStyleValues.Medium;
            sheet.Cell(1, cell + i).Style.Border.LeftBorderColor = XLColor.Black;
            sheet.Cell(1, cell + i).Style.Border.RightBorder = XLBorderStyleValues.Medium;
            sheet.Cell(1, cell + i).Style.Border.RightBorderColor = XLColor.Black;
        }
        sheet.Range(1, cell, 1, cell + 4).Merge();

        AddQuestionBottomCell(sheet, "ответ", cell);
        AddQuestionBottomCell(sheet, "нач. время", cell+1);
        AddQuestionBottomCell(sheet, "кон. время", cell+2);
        AddQuestionBottomCell(sheet, "правильность", cell+3);
        AddQuestionBottomCell(sheet, "поиск в интенете", cell+4);
    }

    protected void AddQuestionBottomCell(IXLWorksheet sheet, string title, int cell)
    {
        sheet.Cell(2, cell).Value = title;
        sheet.Cell(2, cell).Style.Border.BottomBorder = XLBorderStyleValues.Medium;
        sheet.Cell(2, cell).Style.Border.BottomBorderColor = XLColor.Black;
        sheet.Cell(2, cell).Style.Border.LeftBorder = XLBorderStyleValues.Medium;
        sheet.Cell(2, cell).Style.Border.LeftBorderColor = XLColor.Black;
        sheet.Cell(2, cell).Style.Border.RightBorder = XLBorderStyleValues.Medium;
        sheet.Cell(2, cell).Style.Border.RightBorderColor = XLColor.Black;
    }

    protected void SimpleHeaderPart(IXLWorksheet sheet, int CellColumn, int CellRow, string CellValue, int Width = 10)
    {
        sheet.Cell(CellRow, CellColumn).Value = CellValue;
        sheet.Column(CellColumn).Width = Width;
        sheet.Cell(CellRow, CellColumn).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        sheet.Cell(CellRow, CellColumn).Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
        sheet.Cell(CellRow, CellColumn).Style.Border.RightBorder = XLBorderStyleValues.Medium;
        sheet.Cell(CellRow, CellColumn).Style.Border.RightBorderColor = XLColor.Black;
        sheet.Cell(CellRow+1, CellColumn).Style.Border.RightBorder = XLBorderStyleValues.Medium;
        sheet.Cell(CellRow+1, CellColumn).Style.Border.RightBorderColor = XLColor.Black;
        sheet.Cell(CellRow+1, CellColumn).Style.Border.BottomBorder = XLBorderStyleValues.Medium;
        sheet.Cell(CellRow+1, CellColumn).Style.Border.BottomBorderColor = XLColor.Black;
        sheet.Range(CellRow, CellColumn, CellRow + 1, CellColumn).Merge();
    }
}
