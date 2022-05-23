using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace telebot
{
    internal class CommandRepository
    {
        public bool OK { get; set; } = false;
        public bool IsWaitingUserInput { get; set; } = false;
        public string commandName { get; set; } = "/";
    }
}
