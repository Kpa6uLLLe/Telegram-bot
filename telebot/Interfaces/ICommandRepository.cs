using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace telebot
{
    internal interface ICommandRepository
    {
        public void Add(Command command, long userId);
        public void Remove(long userId);
        public Command Get(long userId);

    }
}
