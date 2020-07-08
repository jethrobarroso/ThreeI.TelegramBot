using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using ThreeI.TelegramBot.Core;
using ThreeI.TelegramBot.Windows;

namespace ThreeI.TelegramBot.Test.Core
{
    public class TelegramBotManagerTests
    {
        private IConfiguration _config;
        private string _token;

        [OneTimeSetUp]
        public void InitialSetup()
        {
            _config = ConfigHelper.InitConfiguration();
            _token = _config["TelegramToken"];
        }

        [Test]
        public void Init_ArgumentException_BadToken()
        {
            Assert.Throws<ArgumentException>(() => new TelegramBotManager("Bad Token"));
        }

        [Test]
        public void StartReceiving_TrueFalse_StartStopReceiving()
        {
            IBotManager bot = new TelegramBotManager(_token);
            bot.StartReceiving();

            Assert.That(() =>
            {
                bot.StartReceiving();
                return bot.IsReceiving;
            });

            Assert.That(() =>
            {
                bot.StopReceiving();
                return bot.IsReceiving;
            }, Is.False);
        }
    }
}
