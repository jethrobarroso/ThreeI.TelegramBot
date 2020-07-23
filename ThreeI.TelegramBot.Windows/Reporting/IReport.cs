using System.Collections.Generic;
using System.Threading.Tasks;
using ThreeI.TelegramBot.Core;

namespace ThreeI.TelegramBot.Windows.Reporting
{
    public interface IReport
    {
        /// <summary>
        /// Create an Excel report from a collection of <FaultReport cref="FaultReport"/>
        /// and return the path of the constructed file.
        /// </summary>
        /// <param name="reports">Collection of faults used to create report</param>
        /// <param name="filePath">Path of file to be stored</param>
        /// <returns>File path of created report</returns>
        string CreateExcelReport(IEnumerable<FaultReport> reports, string filePath);

        /// <summary>
        /// Delete the excel file.
        /// </summary>
        /// <returns>Wheter the file was successfully deleted or not.</returns>
        bool TryDeleteExcelReport();
    }
}