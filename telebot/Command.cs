using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace telebot
{
    public class Command
    {
        public bool OK { get; set; } = false;
        public bool completed { get; set; } = false;
        public bool IsWaitingUserInput { get; set; } = false;

        public bool categoryListNeeded { get; set; } = false;

        public string commandName { get; set; } = "/";

        public string category { get; set; } = string.Empty;

        public string link { get; set; } = string.Empty;

        public string message { get; set; } = string.Empty;

        public CommandError commandError { get; set; } = new CommandError();

        public void Error()
        {
            OK = false;
        }
        public void Error(string errorMessage)
        {
            OK = false;
            commandError.errorDescription = errorMessage;

        }
        public void Complete()
        {
            completed = true;
        }
            public void Complete(string completionMessage)
        {
            completed = true;
            message = completionMessage;
        }
    }
}
