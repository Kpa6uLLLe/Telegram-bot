using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
namespace telebot
{
    class CommandHandler
    {
        private CommandFactory commandFactory;
        private CommandRepository commandRepository;
        private IChat _chat;
        private Command currentCommand;
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

        public Command ProcessNewMessage(CustomUpdate update)
        {
            currentCommand = commandRepository.Get(update.Message.Chat.Id);
            if (currentCommand == null)
                currentCommand = new Command();

            if (update.Message.Text[0] == '/' && !currentCommand.IsWaitingUserInput)
            {
                currentCommand = commandFactory.ProcessNewCommand(update);

            }
            else if (update.Message.Text[0] != '/' && currentCommand.IsWaitingUserInput)
            {
                currentCommand = commandFactory.ProcessNewUserInput(update);
                
            }
            //Сделать базовый commandRepository с сообщением об ошибке
            return currentCommand;

        }




    }
}
