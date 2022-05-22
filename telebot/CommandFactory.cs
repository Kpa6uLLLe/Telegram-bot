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
            "/get-links"
        };
        public static readonly string[] restrictedCategories =
        {
            "",
            String.Empty,
            "Все",
            "Всё",
            "All"
        };
        private CommandRepository? awaitingCommand = null;
        private string selectedCategoria = string.Empty;
        public CommandRepository ProcessNewCommand(CustomUpdate update)
        {
            //Фабрика команд
            var response = new CommandRepository();
            if (!possibleCommands.Contains(update.Message.Text))
            {
                return response;
            }

            response.OK = true;
            response.commandName = update.Message.Text; 
            
            switch (response.commandName)
            {
                case "/store-link":
                    response.message = "Пожалуйста, выберите категорию из списка или введите название для новой категории";
                    awaitingCommand = response;
                    break;
                case "/get-links":
                    response.message = "Пожалуйста, выберите категорию из списка";
                    awaitingCommand = response;
                    break;
                default:
                    response = new CommandRepository();
                    awaitingCommand = null;
                    break;
            }
            return response;
            
        }
        public CommandRepository ProcessNewUserInput(CustomUpdate update)
        {
            //Фабрика команд для ввода данных от пользователя
            return new CommandRepository();
        }
    }
}
