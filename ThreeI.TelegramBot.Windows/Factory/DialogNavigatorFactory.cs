using System;
using System.Collections.Generic;
using System.Text;
using ThreeI.TelegramBot.Core;
using ThreeI.TelegramBot.Data;
using ThreeI.TelegramBot.Windows.Chats;

namespace ThreeI.TelegramBot.Windows.Factory
{
    /// <summary>
    /// Factory for Telegram bot message navigators
    /// </summary>
    public class DialogNavigatorFactory
    {
        private readonly IDataRepository _repo;
        private readonly Beneficiary _beneficiary;
        private readonly IMessageProvidor _messageProvidor;

        public DialogNavigatorFactory(IDataRepository repo, Beneficiary beneficiary, IMessageProvidor messageProvidor)
        {
            _repo = repo;
            _beneficiary = beneficiary;
            _messageProvidor = messageProvidor;
        }

        /// <summary>
        /// Factory method to create and return appropriate DialogNavigator
        /// </summary>
        /// <param name="type">Type of DialogNavigator to create</param>
        /// <param name="message">Incoming message to process</param>
        /// <returns></returns>
        public DialogNavigator CreateNavigator(DialogType type, string message)
        {
            switch (type)
            {
                case DialogType.Callback:
                    return new CallbackNavigator(message, _repo, _beneficiary, _messageProvidor);
                case DialogType.Text:
                default:
                    return new TextNavigator(message, _repo, _beneficiary, _messageProvidor);
            }
        }
    }
}
