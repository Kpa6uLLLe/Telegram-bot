using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace telebot
{
    public class StorageEntity
    {
        private string name = "";
        private List<string> LinkList = new List<string>(0);
        public string AddLink(string link)
        {
            LinkList.Add(link);

            return "Success";
        }
        public string AddLinks(string[] link)
        {
            foreach (string linkItem in link)
            {
                AddLink(linkItem);
            }
            return "Success";
        }
        public List<string> GetLinks()
        {
            return LinkList;
        }
        public string GetLinksString()
        {
            string result = name+":\n";
            foreach (string link in LinkList)
                result +="\t\t" + link + "\n";
            return result;
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
    }
}

