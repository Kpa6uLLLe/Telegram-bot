
namespace telebot
{
    public class CommandError
    {
        public string errorName { get; set; } = "Unknown Error";
        public string errorDescription { get; set; } = "Что-то пошло не так...";
        public int errorCode { get; set; } = 0;

        public string GetErrorInfo()
        {
            return "* " + errorCode + "\n" + errorName + "\n" + errorDescription + "\n";
        }
        public string GetMessageForUser()
        {
            return "* " + errorDescription + " *" + "\n";
        }
    }
}
