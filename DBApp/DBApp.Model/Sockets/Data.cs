using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBApp.Model.Sockets
{
    public class Data : ConnectionInfo
    {
        public string UserName { get; set; }
        public string Mseesage { get; set; }
    }
}
