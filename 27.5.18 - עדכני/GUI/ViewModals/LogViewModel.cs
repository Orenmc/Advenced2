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
        public LogViewModel(ILogModal Mymodal)
        {
            model = Mymodal;            
        }

		public void NotifyPropertyChanged(string propName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
		}

		public ObservableCollection<MessageRecievedEventArgs> VM_LogList
        {
            get => this.model.LogList;
            set => throw new NotImplementedException();
    }
            

    }
}
