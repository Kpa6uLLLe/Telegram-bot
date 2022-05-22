
namespace telebot
{
    internal class CommandError
    {
        public string errorName { get; set; } = "Unknown Error";
        public string errorDescription { get; set; } = "We don't know what happened";
        public int errorCode { get; set; } = 0;

        public string GetErrorInfo()
        {
            return "#" + errorCode + "\n" + errorName + "\n" + errorDescription + "\n";
        }

    }
}
