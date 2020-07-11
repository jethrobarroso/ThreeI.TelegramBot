﻿using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using ThreeI.TelegramBot.Core;

namespace ThreeI.TelegramBot.Test.Core
{
    public class ConversationTest
    {
        [Test]
        public void Reset_DefaultAndEmpty()
        {
            var state = new DialogState()
            {
                Block = "value",
                Unit = "value",
                DialogId = 1,
                Description = "value",
                Category = 2,
                Confirmation = 1
            };

            state.Reset(true);

            Assert.That(state.Block, Is.Null);
            Assert.That(state.Unit, Is.Null);
            Assert.That(state.Description, Is.Null);
            Assert.That(state.Category, Is.Zero);
            Assert.That(state.Block, Is.Null);
            Assert.That(state.ChatPhase, Is.EqualTo(1));
        }
    }
}
