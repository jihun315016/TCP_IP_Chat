using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ChatLib.Sockets
{
    public abstract class ChatBase : ChatEventBase
    {
        protected readonly IPAddress IPAddress;
        protected readonly int Port;

        private bool _isRunning;

        public event Action<bool>? RunningStateChanged;

        public ChatBase(IPAddress ipAddress, int port)
        {
            IPAddress = ipAddress;
            Port = port;
        }

        public bool IsRunning
        {
            get => _isRunning;
            set
            {
                if (_isRunning != value)
                {
                    _isRunning = value;
                    RunningStateChanged?.Invoke(_isRunning);
                }
            }
        }
    }
}
