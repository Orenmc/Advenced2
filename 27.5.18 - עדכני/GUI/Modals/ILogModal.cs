using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure;

namespace GUI
{
    interface ILogModal : INotifyPropertyChanged
    {
        ObservableCollection<MessageRecievedEventArgs> LogList { get; set; }
        
    }
}
