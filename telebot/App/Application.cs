using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace telebot
{
    internal class Application
    {
      public void Run(string[] args)
        {
            DBInit dBInit = new DBInit();
            IStorage storage = dBInit._storage;
            CommandHandler handler = new CommandHandler(storage);
            IChat chat = new TelegramBotApiChatHander(handler);
            chat.Start();
        }
    }
}
