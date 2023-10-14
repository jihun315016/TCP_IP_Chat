using ChatLib.Sockets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatLib.Models
{
    public class ChatInfo : ChatConnection
    {
        public ChatState State { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
