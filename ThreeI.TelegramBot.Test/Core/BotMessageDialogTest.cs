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

            Assert.That(messenger.MessageBlock, Is.EqualTo("MessageBlock dialog"));
            Assert.That(messenger.MessageUnit, Is.EqualTo("MessageUnit dialog"));
            Assert.That(messenger.MessageOption, Is.EqualTo("MessageOptions dialog"));
            Assert.That(messenger.MessageDescription, Is.EqualTo("MessageDescription dialog"));
            Assert.That(messenger.MessageFinal, Is.EqualTo("MessageFinal dialog"));
            Assert.That(messenger.MessageConfirm, Is.EqualTo("MessageConfirm dialog"));
        }
    }
}
