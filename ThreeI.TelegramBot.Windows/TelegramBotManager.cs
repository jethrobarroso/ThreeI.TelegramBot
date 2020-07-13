using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;
using ThreeI.TelegramBot.Data;
using ThreeI.TelegramBot.Windows.Factory;

namespace ThreeI.TelegramBot.Windows
{
    public class TelegramBotManager : IBotManager
    {
        private readonly IConfiguration _config;
        private readonly IDataRepository _repo;
        private readonly IMessageProvidor _messageProvidor;

        public TelegramBotManager(IConfiguration config, IDataRepository repo, IMessageProvidor messageProvidor)
        {
            _config = config;
            _repo = repo;
            _messageProvidor = messageProvidor;

            ApiToken = _config["TelegramToken"];
            Bot = new TelegramBotClient(ApiToken);

            InitialiseBotHandlers();
        }

        #region Properties
        public string ApiToken { get; }

        public TelegramBotClient Bot { get; }

        public bool IsReceiving { get; private set; }
        #endregion

        #region Methods
        public void StartReceiving()
        {
            Bot.StartReceiving();
            IsReceiving = true;
        }

        public void StopReceiving()
        {
            Bot.StopReceiving();
            IsReceiving = false;
        }
        #endregion

        #region Callback Methods
        /// <summary>
        /// Registers handlers for the bot to handel regular incoming messages
        /// </summary>
        /// <param name="sender">Message sender</param>
        /// <param name="e">Event arguments containing message details</param>
        private void Bot_OnMessage(object sender, MessageEventArgs e)
        {
            try
            {
                if (e.Message.Type == MessageType.Text)
                {
                    var userId = e.Message.From.Id;
                    var nav = DialogNavigatorFactory.CreateNavigator(DialogType.Text, e.Message.Text, _repo, _messageProvidor, _config);
                    var (isSupervisor, reponse) = nav.SupervisorCheck(e.Message);

                    if (isSupervisor)
                    {
                        Bot.SendTextMessageAsync(e.Message.Chat.Id, reponse, ParseMode.Html);
                    }
                    else
                    {
                        var dialog = nav.ValidateUser(userId.ToString());
                        var supportState = nav.ProcessMessage(dialog, e.Message);
                        Bot.SendTextMessageAsync(e.Message.Chat.Id, supportState.reponse, ParseMode.Html);

                        if (supportState.supportSubmitted)
                        {
                            var message = $"<b>Dear {dialog.Category.Supervisor.FullName}</b> \n\nA support request has been logged " +
                                $"by Unit <i>{dialog.Unit}</i> @ block <i>{dialog.Block}</i> with the following description:\n\n" +
                                $"<pre>{dialog.Description}</pre>";
                            Bot.SendTextMessageAsync(dialog.Category.Supervisor.ChatId, message, ParseMode.Html);
                            dialog.Reset(false);
                            _repo.UpdateDialogState(dialog);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Fatal(ex.Message);
            }
        }
        #endregion

        #region Initialiser Methods
        /// <summary>
        /// Initialize various event handlers for the bot
        /// </summary>
        protected virtual void InitialiseBotHandlers()
        {
            Bot.OnMessage += Bot_OnMessage;
        }
        #endregion

        //private IReplyMarkup CreateKeyboardMarkup()
        //{
        //    var inlineButtons = new InlineKeyboardButton[3][]
        //    {
        //        new [] { new InlineKeyboardButton() { Text = "Button 1", CallbackData = "test" } },
        //        new [] { new InlineKeyboardButton() { Text = "Button 2", CallbackData = "test" } },
        //        new [] { new InlineKeyboardButton() { Text = "Button 3", CallbackData = "test" } },
        //    };

        //    return new InlineKeyboardMarkup(inlineButtons);
        //}
    }
}
