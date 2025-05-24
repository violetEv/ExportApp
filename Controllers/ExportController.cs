using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using Rotativa.AspNetCore;
using System;
using System.Collections.Generic;
using System.IO;
using ClosedXML.Excel;


public class DataModel
{
    public string Nama { get; set; } = string.Empty;
    public DateTime Tanggal { get; set; }
}

public class ExportController : Controller
{
    // Dummy data
    private List<DataModel> AmbilData()
    {
        return new List<DataModel>
        {
            new DataModel { Nama = "Marry", Tanggal = DateTime.Now },
            new DataModel { Nama = "Jane", Tanggal = DateTime.Now }
        };
    }

    public IActionResult Index()
    {
        var data = AmbilData();
        return View(data);
    }

    public IActionResult ExportPdf()
    {
        var data = AmbilData();
        return new ViewAsPdf("ExportPdf", data)
        {
            FileName = "ExportPdf.pdf"
        };
    }

public IActionResult ExportExcel()
    {
        using (var workbook = new XLWorkbook())
        {
            var worksheet = workbook.Worksheets.Add("Data");
            worksheet.Cell(1, 1).Value = "Nama";
            worksheet.Cell(1, 2).Value = "Tanggal";

            worksheet.Cell(2, 1).Value = "Marry";
            worksheet.Cell(2, 2).Value = DateTime.Now.ToString("dd/MM/yyyy");
 
            worksheet.Cell(3, 1).Value = "Jane";
            worksheet.Cell(3, 2).Value = DateTime.Now.ToString("dd/MM/yyyy");
            using (var stream = new MemoryStream())
            {
                workbook.SaveAs(stream);
                stream.Position = 0;

                return File(stream.ToArray(),
                    "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                    "ExportExcel.xlsx");
            }
        }
}
}
