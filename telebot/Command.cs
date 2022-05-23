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
        public bool IsWaitingUserInput { get; set; } = false;
        public string commandName { get; set; } = "/";

        public string category { get; set; } = string.Empty;

        public string message { get; set; } = string.Empty;

        public CommandError commandError = new CommandError();
    }
}
