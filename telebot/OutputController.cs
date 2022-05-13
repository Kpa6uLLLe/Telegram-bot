using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace telebot
{
    internal class OutputController
    {
        public static async Task SendMessageAsync(string message)
        {

        }

        public static async Task SendMessageAsync(LinkData linkData)
        {
            string message = linkData.Name;
            foreach (string s in linkData.GetLinks())
                message += s + "\n";
        }
        public static async Task SendMessageAsync(LinkData[] linkDataArray)
        {
            string message = "";
            foreach (LinkData linkData in linkDataArray)
            {
                message += "\n" + linkData.Name + "\n";
                foreach (string s in linkData.GetLinks())
                    message += s + "\n";
            }
        }
    }
}
