using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using DBApp.Client.Models;
using DBApp.Client.Network;

namespace DBApp.Client.Commands
{
    class ConnCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object? parameter)
        {   
            return true;
        }

        public void Execute(object? parameter)
        {
            Data user = parameter as Data;
            DBAppClient client;

            if (user != null)
            {
                // TODO : 서버 TCP IP 연결 
                client = new DBAppClient(user);
                client.StartAsync();
            }
        }
    }
}
