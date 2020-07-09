using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using ThreeI.TelegramBot.Core;
using Microsoft.Extensions.Configuration;
using ThreeI.TelegramBot.Windows;

namespace ThreeI.TelegramBot.Test.Core
{
    public class BotMessageDialogTest
    {
        private IConfiguration _config;

        [OneTimeSetUp]
        public void InitialSetup()
        {
            _config = TestConfigHelper.InitConfiguration();
        }

        [Test]
        public void Initialise_Success_MessageType()
        {
            IMessageProvidor messenger = new BotMessageDialog(_config);

            Assert.That(messenger.Block, Is.EqualTo("Block dialog"));
            Assert.That(messenger.Unit, Is.EqualTo("Unit dialog"));
            Assert.That(messenger.Category, Is.EqualTo("Category dialog"));
            Assert.That(messenger.Description, Is.EqualTo("Description dialog"));
            Assert.That(messenger.Final, Is.EqualTo("Final dialog"));
            Assert.That(messenger.Confirm, Is.EqualTo("Confirm dialog"));
            Assert.That(messenger.InitialMessage, Is.EqualTo("InitialMessage dialog"));
        }
    }
}
