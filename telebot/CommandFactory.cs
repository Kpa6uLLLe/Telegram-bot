using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace telebot
{
    internal class CommandFactory
    {
        public static readonly string[] possibleCommands = {
            "/store-link",
            "/get-links",
            "/help",
            "/start"
        };
        public static readonly string[] restrictedCategories =
        {
            "",
            String.Empty,
            "Все",
            "Всё",
            "All"
        };
        private string GetCommandList()
        {
            string list = "";
            foreach (string command in possibleCommands)
                list+=command+ "\n";
            return list;
        }
        private Command? awaitingCommand = null;
        public Command ProcessNewCommand(CustomUpdate update)
        {
            //Фабрика команд
            var response = new Command();
            if (!possibleCommands.Contains(update.Message.Text))
            {
                return response;
            }

            response.OK = true;
            response.commandName = update.Message.Text; 
            
            switch (response.commandName)
            {
                case "/store-link":
                    response.message = "Пожалуйста, выберите категорию из списка или введите название для новой категории:\n";
                    response.categoryListNeeded = true;
                    response.IsWaitingUserInput = true;
                    awaitingCommand = response;
                    break;
                case "/get-links":
                    response.message = "Пожалуйста, выберите категорию из списка";
                    response.categoryListNeeded = true;
                    response.IsWaitingUserInput = true;
                    awaitingCommand = response;
                    break;
                case "/help":
                    response.message = "Список команд:\n" + GetCommandList();
                    break;
                case "/start":
                    response.message = "Привет!\n Вот список команд:\n" + GetCommandList();
                    break;
                default:
                    response = new Command();
                    awaitingCommand = null;
                    break;
            }
            return response;
            
        }
        public Command ProcessNewUserInput(CustomUpdate update)
        {

            //Фабрика команд для ввода данных от пользователя
            if (restrictedCategories.Contains(update.Message.Text) && awaitingCommand.commandName == "/store-link" && awaitingCommand.category == String.Empty)
            {
                Command command = awaitingCommand;
                command.Error("Эта категория зарезервирована");
                awaitingCommand = null;
                return command;
            }


            if (awaitingCommand.category == String.Empty)
            {
                awaitingCommand.category = update.Message.Text;
                switch (awaitingCommand.commandName)
                {
                    case "/store-link":
                        awaitingCommand.message = "Пожалуйста, введите ссылку";
                        break;
                    case "/get-links":
                        break;
                    default:
                        awaitingCommand.Error();
                        break;
                }
                return awaitingCommand;
            }

            if (awaitingCommand.link == String.Empty) 
            { 
                switch (awaitingCommand.commandName)
                {
                    case "/store-link":
                        awaitingCommand.link = update.Message.Text;
                        break;
                    default:
                        awaitingCommand.Error("No such command yet...");
                        break;
                }
                return awaitingCommand;
            }
            return awaitingCommand;
        }
    }
}
