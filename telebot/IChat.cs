using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace telebot
{
    public interface IChat
    {

        public Task NewChatMessageReceived(CustomUpdate update);

        public Task PostMessageToChat(string message);

        public Task Start();

        public void Stop();

     
    }
}
