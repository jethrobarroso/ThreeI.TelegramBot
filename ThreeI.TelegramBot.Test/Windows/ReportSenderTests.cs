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
            _config = TestConfigInitialiser.InitConfiguration();
        }

        [Test]
        public void SendReportMail_200_WithSnags()
        {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Temp\snag_report.xlsx";
            IMailer mailer = new ReportSender(_config);
            mailer.SendReportMail(path, () => false);

            Assert.Pass();
        }

        [Test]
        public void SendReportMail_200_WithoutSnags()
        {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Temp\snag_report.xlsx";
            IMailer mailer = new ReportSender(_config);
            mailer.SendReportMail(path, () => true);

            Assert.Pass();
        }
    }
}
