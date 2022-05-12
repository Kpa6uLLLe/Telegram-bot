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
            IChat chat = new TelegramBotApiChatHander();
            IStorage storage = new MemoryStorage();
            CommandHandler handler = new CommandHandler(chat, storage);
            chat.Start();
        }
    }
}
