using DBApp.Client.Commands;
using DBApp.Server.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DBApp.Client.ViewModels
{    
    class DBConnViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private Data _user;

        public Data User
        {
            get { return _user; }
            set
            {
                _user = value;
                OnPropertyChanged();
            }
        }       

        public ICommand ConnCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public DBConnViewModel()
        {
            User = new Data();
            ConnCommand = new ConnCommand();
            //CancelCommand = new CancelCommand(user);
        }

        public void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
