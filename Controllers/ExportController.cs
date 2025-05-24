using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using Rotativa.AspNetCore;
using System.IO;

public class ExportController : Controller
{
    public IActionResult Index() => View();

    public IActionResult ExportPdf()
    {
        var model = new { Name = "Contoh Data", Date = DateTime.Now };
        return new ViewAsPdf("ExportPdf", model)
        {
            FileName = "ExportedFile.pdf"
        };
    }

    public IActionResult ExportExcel()
    {
        var stream = new MemoryStream();
        using (var package = new ExcelPackage(stream))
        {
            var sheet = package.Workbook.Worksheets.Add("Data");
            sheet.Cells[1, 1].Value = "Nama";
            sheet.Cells[1, 2].Value = "Tanggal";
            sheet.Cells[2, 1].Value = "Contoh Data";
            sheet.Cells[2, 2].Value = DateTime.Now.ToString("dd/MM/yyyy");
            package.Save();
        }
        stream.Position = 0;
        string excelName = $"Export_{DateTime.Now:yyyyMMddHHmmss}.xlsx";
        return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
    }
}
