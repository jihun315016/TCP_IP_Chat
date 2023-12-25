using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBApp.Model.Sockets
{
    public class ConnectionInfo
    {
        public string DBServer { get; set; }
        public string DBName { get; set; }
        public string Address { get; set; }
        public string Port { get; set; }
    }
}
