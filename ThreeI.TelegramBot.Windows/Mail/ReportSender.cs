using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Spreadsheet;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ThreeI.TelegramBot.Windows.Mail
{
    public class ReportSender : IMailer
    {
        private readonly IConfiguration _config;

        /// <summary>
        /// Initialise instance with required recipient address and fullname.
        /// </summary>
        /// <param name="to">Recipient mail address</param>
        /// <param name="fullname">Full name of the recipient</param>
        public ReportSender(IConfiguration config)
        {
            _config = config;
        }

        public void SendReportMail(string path)
        {
            //var jsonString = _config.GetSection("ReportEmail").Value;
            //var mailConf = JsonConvert.DeserializeObject<EmailConfig>(jsonString);

            var mailConf = _config.GetSection("ReportEmail").Get<EmailConfig>();
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(mailConf.FromName, mailConf.From));
            message.To.Add(new MailboxAddress(mailConf.ToName, mailConf.To));
            message.Subject = $"Snags Report - {DateTime.Today.ToShortDateString()}";

            var builder = new BodyBuilder();
            builder.TextBody = $"Dear {mailConf.ToName},\n\n" +
                $"Please see attached support requests for the past 24 hours.\n\n" +
                $"Kind regards\nYour ThreeI Telegram Bot";

            builder.Attachments.Add(path);

            message.Body = builder.ToMessageBody();

            try
            {
                using (var client = new SmtpClient())
                {
                    client.Connect("mail.a-i-solutions.co.za", 465, true);
                    client.Authenticate("telegrambot@a-i-solutions.co.za", "q0LeWxfTX2zb");
                    client.Send(message);
                    client.Disconnect(true);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, "Mail send error");
                throw;
            }
        }
    }
}
