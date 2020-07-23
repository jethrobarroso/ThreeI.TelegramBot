using ClosedXML.Excel;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Enumeration;
using System.Linq;
using System.Text;
using ThreeI.TelegramBot.Core;

namespace ThreeI.TelegramBot.Windows.Reporting
{
    public class ExcelConstructor : IReport
    {
        private readonly string[] _headers = { "ID", "BLOCK", "UNIT", "FIRSTNAME", "LASTNAME", "DESCRIPTION", "DATELOGGED" };

        public ExcelConstructor() { }

        public ExcelConstructor(string filePath, Guid fileId)
        {
            FilePath = filePath;
            FileId = fileId;
        }

        public Guid FileId { get; }
        public string FilePath { get; }

        /// <summary>
        /// Create an Excel report from a collection of <FaultReport cref="FaultReport"/>
        /// and return the path of the constructed file.
        /// </summary>
        /// <param name="reports">Collection of faults used to create report</param>
        /// <param name="filePath">Path of file to be stored</param>
        /// <returns>File path of created report</returns>
        public string CreateExcelReport(IEnumerable<FaultReport> reports, string filePath)
        {
            var guid = Guid.NewGuid();
            string fullPathFile = $@"{filePath}\3i_attachment-{guid}.xlsx";
            using (var wb = new XLWorkbook())
            {
                var ws = wb.Worksheets.Add("Snags Report");

                for (int i = 0; i < _headers.Length; i++)
                    ws.Cell(1, i + 1).Value = _headers[i];

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

        /// <summary>
        /// Delete the excel file.
        /// </summary>
        /// <returns>Wheter the file was successfully deleted or not.</returns>
        public bool TryDeleteExcelReport()
        {
            bool result;
            if (File.Exists(FilePath))
            {
                File.Delete(FilePath);
                result = true;
            }    
            else
            {
                Log.Error($"Unable to delete temp excel file @{FilePath}\nFile may not exist " +
                    "or it is open by another application");
                result = false;
            }

            return result;
        }
    }
}
