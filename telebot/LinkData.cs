using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace telebot
{
    internal class LinkData
    {
        private string name = "";
        private List<string> LinkList = new List<string>(5);
        public string AddLinks(string link)
        {
            LinkList.Append(link);

            return "Success";
        }
        public string AddLinks(string[] link)
        {
            foreach(string linkItem in link)
            {
                LinkList.Append(linkItem);
            }
            return "Success";
        }
        public List<string> GetLinks()
        {
            return LinkList;
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
    }
}
