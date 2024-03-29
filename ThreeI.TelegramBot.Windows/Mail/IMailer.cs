﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ThreeI.TelegramBot.Core;

namespace ThreeI.TelegramBot.Windows.Mail
{
    public interface IMailer
    {
        /// <summary>
        /// Sends an email to the recipient (configured in appsettings) with a .xlsx document
        /// containing the snags captured for past 24 hours
        /// </summary>
        /// <param name="path">Path to the .xlsx document as an email attachment</param>
        /// <param name="emptyCondition">Condition whether there were any snags recorded.</param>
        void SendReportMail(string path, Func<bool> emptyCondition);

        /// <summary>
        /// Sends an email to the configured recipient containing 
        /// extracted <see cref="FaultReport"/> information.
        /// </summary>
        /// <param name="fault">The <see cref="FaultReport"/> object that 
        /// was logged</param>
        void SendLoggedFault(FaultReport fault);
    }
}
