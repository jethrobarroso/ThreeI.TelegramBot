using NUnit.Framework;
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
                Category = new Category(),
                Confirmation = 1
            };

            state.Reset(true);

            Assert.That(state.Block, Is.Null);
            Assert.That(state.Unit, Is.Null);
            Assert.That(state.Description, Is.Null);
            Assert.That(state.Block, Is.Null);
            Assert.That(state.ChatPhase, Is.EqualTo(1));

            state.Reset(false);

            Assert.That(state.IsSupportMode, Is.False);
        }
    }
}
