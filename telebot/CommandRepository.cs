using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using Microsoft.EntityFrameworkCore;

namespace telebot
{
    internal class CommandRepository : ICommandRepository
    {
        Dictionary<long, Command> _commands;
        public CommandRepository()
        {
            _commands = new Dictionary<long, Command>();
        }
        public Command Get(long userId)
        {
            if(_commands.ContainsKey(userId))
            return _commands[userId];
            return null;
        }

        public void Add(Command command, long userId)
        {
            _commands.Add(userId, command);
        }
        public void Remove(long userId)
        {
           _commands.Remove(userId);
        }
    }
}
