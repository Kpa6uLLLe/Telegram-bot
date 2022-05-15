using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace telebot
{
    internal class TelegramBotApiChatHander : IChat
    {
        private string token = "";
        private CancellationTokenSource cts;
        private CancellationToken cancellationToken;
        private TelegramBotClient botClient;
        private ReceiverOptions receiverOptions;
        private CommandHandler _commandHandler;
        private CustomUpdate _update;
        public TelegramBotApiChatHander(CommandHandler commandHandler)
        {
            CustomUpdate _update = new CustomUpdate();
            _commandHandler = commandHandler;
            _commandHandler.SetIChat(this);
            AppSettings settings = new AppSettings();
            token = settings.GetToken();
            botClient = new TelegramBotClient(token);

            Start();
            Console.ReadLine();
            Stop();

        }
        public async Task NewChatMessageReceived(CustomUpdate update)
        {
            var response = _commandHandler.ProcessNewMessage(update);
            if (response.OK)
                Console.WriteLine($"Successfully handled '{response.commandName}' from {update.Message.Chat.FirstName} {_update.Message.Chat.LastName}");
            else
                Console.WriteLine($"Incorrect input in chat#{_update.Message.Chat.Id}");


        }

        public async Task PostMessageToChat(string message)
        {
            await botClient.SendTextMessageAsync(_update.Message.Chat,message);
            Console.WriteLine($"Sent message: '{_update.Message.Text}' to {_update.Message.Chat.LastName} {_update.Message.Chat.LastName} in chat#{_update.Message.Chat.Id}");
        }

        public async Task Start()
        {
           cts = new CancellationTokenSource();
           cancellationToken = cts.Token;
            receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = Array.Empty<UpdateType>()
            };
            var updateReceiver = new QueuedUpdateReceiver(botClient, receiverOptions);
           botClient.StartReceiving(
                    HandleUpdateAsync,
                    HandlePollingErrorAsync,
                    receiverOptions,
                    cancellationToken
                );
            Console.WriteLine("Started");

        }
        private static CustomUpdate ConvertUpdate(Update update)
        {
            CustomUpdate customUpdate = new CustomUpdate();
            customUpdate.Message = update.Message;
            customUpdate.Id = update.Id;
            return customUpdate;
        }
        async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            _update = ConvertUpdate(update);
            if (_update.Type == UpdateType.Message && _update.Message.Type == MessageType.Text && _update.Message.Text != null)
                await NewChatMessageReceived(_update);
            System.Threading.Thread.Sleep(500);
        }
        Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException
                    => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };

            Console.WriteLine(ErrorMessage);
            return Task.CompletedTask;

        }
        public void Stop()
        {
            cts.Cancel();

        }


    }
}
