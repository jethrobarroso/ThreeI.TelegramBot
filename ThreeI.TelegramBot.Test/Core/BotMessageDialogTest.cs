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
            _config = ConfigHelper.InitConfiguration();
        }

        [Test]
        public void Initialise_Success_MessageType()
        {
            IMessageProvidor messenger = new BotMessageDialog(_config);

            Assert.That(messenger.Block, Is.EqualTo("MessageBlock dialog"));
            Assert.That(messenger.Unit, Is.EqualTo("MessageUnit dialog"));
            Assert.That(messenger.Category, Is.EqualTo("MessageOptions dialog"));
            Assert.That(messenger.Description, Is.EqualTo("MessageDescription dialog"));
            Assert.That(messenger.Final, Is.EqualTo("MessageFinal dialog"));
            Assert.That(messenger.Confirm, Is.EqualTo("MessageConfirm dialog"));
        }
    }
}
