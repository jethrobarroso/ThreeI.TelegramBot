using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ThreeI.TelegramBot.Windows.Mail
{
    public interface IMailer
    {
        /// <summary>
        /// Send email with an FaultReports excel attachement.
        /// </summary>
        /// <param name="path">Path to Excel file</param>
        void SendReportMail(string path);
    }
}
