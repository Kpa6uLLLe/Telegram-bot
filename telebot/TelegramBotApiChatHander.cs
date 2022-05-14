using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace telebot
{
    internal class TelegramBotApiChatHander : IChat 
    {
        private string token = "";
        public TelegramBotApiChatHander()
        {
            AppSettings settings = new AppSettings();
            token = settings.GetToken();

        }
        public void NewChatMessageReceived()
        {

        }

        public void PostMessageToChat()
        {

        }

        public void Start()
        {

        }

        public void Stop()
        {

        }


    }
}
