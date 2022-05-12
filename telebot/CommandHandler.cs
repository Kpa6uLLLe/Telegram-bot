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
       public CommandHandler(IChat chat, IStorage storage)
        {
            this.commandFactory = new CommandFactory();
        }
    }
}
