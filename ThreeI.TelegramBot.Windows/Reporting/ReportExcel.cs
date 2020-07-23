using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ThreeI.TelegramBot.Core;

namespace ThreeI.TelegramBot.Windows.Reporting
{
    public class ReportExcel : IReport
    {
        private readonly string[] headers = { "ID", "BLOCK", "UNIT", "FIRSTNAME", "LASTNAME", "DESCRIPTION",  "DATELOGGED" };

        public string CreateExcelReport(IEnumerable<FaultReport> reports, string filePath)
        {
            using (var wb = new XLWorkbook())
            {
                var ws = wb.Worksheets.Add("Snags Report");

                for (int i = 0; i < headers.Length; i++)
                    ws.Cell(1, i + 1).Value = headers[i];

                for (int i = 0; i < reports.Count(); i++)
                {
                    ws.Cell(i + 2, 1).Value = reports.ElementAt(i).Id;
                    ws.Cell(i + 2, 2).Value = reports.ElementAt(i).Block;
                    ws.Cell(i + 2, 3).Value = reports.ElementAt(i).Unit;
                    ws.Cell(i + 2, 4).Value = reports.ElementAt(i).FirstName;
                    ws.Cell(i + 2, 5).Value = reports.ElementAt(i).LastName;
                    ws.Cell(i + 2, 6).Value = reports.ElementAt(i).Description;
                    ws.Cell(i + 2, 7).Value = reports.ElementAt(i).DateLogged;
                }

                wb.SaveAs(filePath);

                return filePath;
            };
        }

        public void DeleteExcelReport(string path)
        {
            throw new NotImplementedException();
        }
    }
}
