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
        private Command? awaitingCommand = null;
        private string selectedCategoria = string.Empty;
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
                    response.message = "Пожалуйста, выберите категорию из списка или введите название для новой категории";
                    awaitingCommand = response;
                    break;
                case "/get-links":
                    response.message = "Пожалуйста, выберите категорию из списка";
                    awaitingCommand = response;
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
            return new Command();
        }
    }
}
