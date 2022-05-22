using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace telebot
{
    class CommandHandler
    {
        private CommandFactory commandFactory;
        private CommandRepository commandRepository;
        private IChat _chat;
        private bool IsWaitingUserInput = false;
        private IStorage _storage;
        public CommandHandler(IStorage storage)
        {
            _storage = storage;
            commandFactory = new CommandFactory();
            commandRepository = new CommandRepository();
        }
        public void SetIChat(IChat chat)
        {
            _chat = chat;
        }

        public CommandRepository ProcessNewMessage(CustomUpdate update)
        {
            var response = new CommandRepository();
            if (update.Message.Text[0] == '/' && !IsWaitingUserInput)
            {
                response = commandFactory.ProcessNewCommand(update);

            }
            else if (update.Message.Text[0] != '/' && IsWaitingUserInput)
            {
                response = commandFactory.ProcessNewUserInput(update);
                
            }
            IsWaitingUserInput = response.IsWaitingUserInput;
            //Сделать базовый commandRepository с сообщением об ошибке
            return response;

        }




    }
}
