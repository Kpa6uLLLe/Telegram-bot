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
            {
                currentCommand = new Command();
                commandRepository.Add(currentCommand, update.Message.Chat.Id);
            }
            if (!_storage.UserExist(update.Message.Chat.Id) && currentCommand.commandName!= "/register")
            {
                update.Message.Text = "/register";
            }
            if(_storage.UserExist(update.Message.Chat.Id) && (currentCommand.commandName == "/register" || update.Message.Text == "/register"))
            {
                currentCommand.Error("You've already registered!");
                commandRepository.Remove(update.Message.Chat.Id);
                return currentCommand;
            }
            if (update.Message.Text[0] == '/' && !currentCommand.IsWaitingUserInput)
            {
                currentCommand = commandFactory.ProcessNewCommand(update);

            }
            else if (update.Message.Text[0] != '/' && currentCommand.IsWaitingUserInput)
            {
                currentCommand = commandFactory.ProcessNewUserInput(update);

            }
           else if (update.Message.Text[0] == '/' && currentCommand.IsWaitingUserInput)
            {
                currentCommand.Error("С \"\\\" обычно начинаются команды...");
            }
            else
            {
                currentCommand.Error("");
            }
                if (currentCommand.categoryListNeeded)
            {
                if (currentCommand.commandName == "/get-links")
                    currentCommand.message += "\nВсе";
                currentCommand.message += _storage.GetEntityNames(update.Message.Chat.Id);
                currentCommand.categoryListNeeded = false;
            }

            if (currentCommand.link != string.Empty && currentCommand.commandName == "/store-link")
            {
                StorageEntity linkData = _storage.GetEntity(currentCommand.category, update.Message.Chat.Id);
                if (linkData == null)
                {
                    linkData = new StorageEntity();
                    linkData.Name = currentCommand.category;
                }
                linkData.AddLink(currentCommand.link);
                _storage.StoreEntity(linkData, update.Message.Chat.Id);
                currentCommand.Complete("Дело сделано");
            }
            if (currentCommand.commandName == "/register" && currentCommand.category != String.Empty)
            {
                switch (currentCommand.link)
                {
                    case "":
                        break;
                    default:
                        _storage.CreateNewUser(update.Message.Chat.Id, update.Message.Chat.FirstName, update.Message.Chat.LastName, currentCommand.category, currentCommand.link);
                        if (!_storage.UserExist(update.Message.Chat.Id))
                            currentCommand.Error("Ошибка при регистрации. Введены некорректные данные или пользователь с таким именем уже существует");
                        currentCommand.Complete("Вы зарегистрированы");
                        break;

                }
            }
                if (currentCommand.commandName == "/get-links")
            {
                switch (currentCommand.category)
                {
                    case "":
                        break;
                    case "Все":
                        currentCommand.message = _storage.GetEntityList(update.Message.Chat.Id);
                        currentCommand.Complete();
                        break;
                    default:
                        StorageEntity entity = _storage.GetEntity(currentCommand.category, update.Message.Chat.Id);
                        if (entity != null)
                        {
                            currentCommand.message = entity.GetLinksString();
                            currentCommand.Complete();
                        }
                        else
                            currentCommand.Error("No such category");
                        break;
                }
            }

            commandRepository.Replace(currentCommand, update.Message.Chat.Id);


            if (!currentCommand.OK || currentCommand.completed)
            {
                commandRepository.Remove(update.Message.Chat.Id);
                return currentCommand;
            }

            return currentCommand;

        }




    }
}
