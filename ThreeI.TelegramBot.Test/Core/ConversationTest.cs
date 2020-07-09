using NUnit.Framework;
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
                Id = 1,
                Description = "value",
                Option = 2,
                Confirmation = 1
            };

            state.Reset();

            Assert.That(state.Block, Is.Null);
            Assert.That(state.Unit, Is.Null);
            Assert.That(state.Description, Is.Null);
            Assert.That(state.Option, Is.Zero);
            Assert.That(state.Block, Is.Null);
            Assert.That(state.ChatPhase, Is.EqualTo(1));
        }
    }
}
