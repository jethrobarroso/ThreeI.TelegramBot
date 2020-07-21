using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using System;
using ThreeI.TelegramBot.Data;
using ThreeI.TelegramBot.Windows;

namespace ThreeI.TelegramBot.Test.Core
{
    public class TelegramBotManagerTest
    {
        private IConfiguration _config;
        private string _token;

        [OneTimeSetUp]
        public void InitialSetup()
        {
            _config = TestConfigInitialiser.InitConfiguration();
            _token = _config["TelegramToken"];
        }

        [Test]
        public void Init_ArgumentException_BadToken()
        {
            var mockRepo = new Mock<IDataRepository>().Object;
            var mockMsgProvider = new Mock<IMessageProvidor>().Object;
            var mockConfig = new Mock<IConfiguration>();
            var mockScope = new Mock<IServiceScopeFactory>().Object;
            mockConfig.Setup(conf => conf["TelegramToken"]).Returns("Bad Token");

            Assert.Throws<ArgumentException>(() => new TelegramBotManager(mockConfig.Object, mockRepo, mockMsgProvider, mockScope));
        }

        [Test]
        public void StartReceiving_TrueFalse_StartStopReceiving()
        {
            var mockRepo = new Mock<IDataRepository>().Object;
            var mockMsgProvider = new Mock<IMessageProvidor>().Object;
            var mockScope = new Mock<IServiceScopeFactory>().Object;
            IBotManager bot = new TelegramBotManager(_config, mockRepo, mockMsgProvider, mockScope);
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
