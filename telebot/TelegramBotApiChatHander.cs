﻿using Telegram.Bot;
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
        public TelegramBotApiChatHander(CommandHandler commandHandler)
        {
            CustomUpdate _update = new CustomUpdate();
            _commandHandler = commandHandler;
            _commandHandler.SetIChat(this);
            AppSettings settings = new AppSettings();
            token = settings.GetToken();
            botClient = new TelegramBotClient(token);

            Start();
            Console.ReadKey();
            Stop();

        }
        public async Task NewChatMessageReceived(CustomUpdate update)
        {
            var response = _commandHandler.ProcessNewMessage(update);
            if (response.OK)
            {
                Console.WriteLine($"Successfully handled '{response.commandName} {response.category} {response.link}' from {update.Message.Chat.FirstName} {update.Message.Chat.LastName}");
                await PostMessageToChat(response.message, update);
            }
            else
            {
                Console.WriteLine($"Incorrect input in chat#{update.Message.Chat.Id}");
                Console.WriteLine(response.commandError.GetErrorInfo());
                if(response.commandError.errorDescription != string.Empty)
                await PostMessageToChat(response.commandError.GetMessageForUser(), update);
            }


        }

        public async Task PostMessageToChat(string message, CustomUpdate update)
        {
            await botClient.SendTextMessageAsync(update.Message.Chat,message);
            Console.WriteLine($"Sent message: '{message}' to {update.Message.Chat.FirstName} {update.Message.Chat.LastName} in chat#{update.Message.Chat.Id}");
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
            CustomUpdate _update = ConvertUpdate(update);
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
