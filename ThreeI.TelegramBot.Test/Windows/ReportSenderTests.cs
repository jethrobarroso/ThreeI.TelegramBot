using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using ThreeI.TelegramBot.Windows.Mail;

namespace ThreeI.TelegramBot.Test.Windows
{
    public class ReportSenderTests
    {
        private IConfiguration _config;

        [OneTimeSetUp]
        public void InitialSetup()
        {
            _config = TestConfigHelper.InitConfiguration();
        }

        [Ignore("Actually sends an email")]
        public void SendReportMail_200()
        {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Temp\snag_report.xlsx";  
            IMailer mailer = new ReportSender(_config);
            mailer.SendReportMail(path);

            Assert.Pass();
        }
    }
}
