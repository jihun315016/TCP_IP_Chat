using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatService.Client
{
    public class Data
    {
        public string Name { get; set; }
        public string Message { get; set; }

        public ConnectionState State { get; set; } = ConnectionState.None;
    }


    public enum ConnectionState
    {
        None,
        Initial,
        Connect,
        Disconnect
    }
}
