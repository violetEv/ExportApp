using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using Rotativa.AspNetCore;
using System;
using System.Collections.Generic;
using System.IO;
using ClosedXML.Excel;
using ExportApp.Data;
using ExportApp.Models;


public class ExportController : Controller
{
        private readonly IDataRepository _repository;

        public ExportController(IDataRepository repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _repository.GetDataAsync();
            return View(data);
        }

    public async Task<IActionResult> ExportPdf()
    {
        var data = await _repository.GetDataAsync();
        return new ViewAsPdf("ExportPdf", data)
        {
            FileName = "ExportPdf.pdf"
        };
    }

public async Task<IActionResult> ExportExcel()
    {
        var data = await _repository.GetDataAsync();

        using (var workbook = new XLWorkbook())
        {
            var worksheet = workbook.Worksheets.Add("Data");

            // Header
            worksheet.Cell(1, 1).Value = "Nama";
            worksheet.Cell(1, 2).Value = "Tanggal";

            // Data rows
            int row = 2;
            foreach (var item in data)
            {
                worksheet.Cell(row, 1).Value = item.Nama;
                worksheet.Cell(row, 2).Value = item.Tanggal.ToString("dd/MM/yyyy");
                row++;
            }

            using (var stream = new MemoryStream())
            {
                workbook.SaveAs(stream);
                stream.Position = 0;

                return File(
                    stream.ToArray(),
                    "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                    "ExportExcel.xlsx"
                );
            }
        }
    }
}
