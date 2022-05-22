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

        public CommandRepository ProcessNewCommand(CustomUpdate update)
        {
            //Фабрика команд
            var response = new CommandRepository();
            if (possibleCommands.Contains(update.Message.Text))
            {
                response.OK = true;
                response.commandName = update.Message.Text; 
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
