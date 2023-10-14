﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatLib.Models
{
    public class ChatMessage
    {
        public ChatState State { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
