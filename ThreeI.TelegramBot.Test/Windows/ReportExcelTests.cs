using DocumentFormat.OpenXml.ExtendedProperties;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ThreeI.TelegramBot.Core;
using ThreeI.TelegramBot.Windows.Mail;
using ThreeI.TelegramBot.Windows.Reporting;

namespace ThreeI.TelegramBot.Test.Windows
{
    public class ReportExcelTests
    {
        private List<FaultReport> _faults;
        private IConfiguration _config;
        private readonly string _fileLocation = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Temp\snag_report.xlsx";

        [OneTimeSetUp]
        public void InitialSetup()
        {
            _config = TestConfigInitialiser.InitConfiguration();
            InitialiseFaults();
        }

        [Test]
        public void CreateExcelReport_FileCreated_ValidData()
        {
            IReport report = new ExcelConstructor();
            var filePath = report.CreateExcelReport(_faults, _fileLocation);

            Assert.That(File.Exists(filePath), Is.True);
        }

        private void InitialiseFaults()
        {
            _faults = new List<FaultReport>()
            {
                new FaultReport()
                {
                    Id = 1,
                    Block = "1401",
                    Unit = "999",
                    Category = new Category() { Description = "Electricity" },
                    Description = "Something's wrong with the lights",
                    FirstName = "John",
                    LastName = "Doe",
                    DateLogged = DateTime.Now,
                },
                new FaultReport()
                {
                    Id = 2,
                    Block = "1404",
                    Unit = "888",
                    Category = new Category() { Description = "Water" },
                    Description = "Something's wrong with the pipes",
                    FirstName = "Jane",
                    LastName = "Doe",
                    DateLogged = DateTime.Now,
                },
                new FaultReport()
                {
                    Id = 3,
                    Block = "1404",
                    Unit = "777",
                    Category = new Category() { Description = "Paint" },
                    Description = "Something's wrong with the paint",
                    FirstName = "Jonathan",
                    LastName = "Joestark",
                    DateLogged = DateTime.Now,
                },
            };
        }
    }
}
