using ChatLib.Handler;
using ChatLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatLib.Events
{
    public class ChatEventArgs : EventArgs
    {
        public ClientHandler ClientHandler { get; set; }
        public ChatInfo ChatInfo { get; set; }

        public ChatEventArgs(ClientHandler clientHandler, ChatInfo chatInfo)
        {
            ChatInfo = chatInfo;
            ClientHandler = clientHandler;
        }
    }
}
