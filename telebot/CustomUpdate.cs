using Telegram.Bot.Types;
// классы других мессенджеров
namespace telebot
{
    public class CustomUpdate : Telegram.Bot.Types.Update //наследовать customUpdate от нужного класса SDK
    {
        public bool IsUserNew { get; set; } = true; 
    }
}
