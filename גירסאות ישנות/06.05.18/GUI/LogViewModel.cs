using System.Collections.ObjectModel;
using System.ComponentModel;
using Infrastructure;
using System;

namespace GUI
{
    class LogViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;


        private ILogModal model;
        public LogViewModel(ILogModal modal)
        {
            this.model = modal;            
        }
        
        public void NotifyPropertyChanged(string PropName)
        {

        }

        public ObservableCollection<MessageRecievedEventArgs> VMLogList
        {
            get => this.model.LogList;
            set => throw new NotImplementedException();
    }
            

    }
}
