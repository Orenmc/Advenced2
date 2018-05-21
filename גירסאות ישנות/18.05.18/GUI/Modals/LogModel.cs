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
    class LogModel : ILogModal
    {
        private GuiClient client;
        private ObservableCollection<MessageRecievedEventArgs> logList;
        public ObservableCollection<MessageRecievedEventArgs> LogList { get => this.logList; set => throw new NotImplementedException(); }


        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        public LogModel()
        {

            client = GuiClient.GetInstance;
            string s = client.SendCommandToServer(CommandStateEnum.GET_ALL_LOG);
            string[] test = s.Split('%');

            logList = new ObservableCollection<MessageRecievedEventArgs>();
            foreach (string e in test)
            {
                logList.Add(new MessageRecievedEventArgs() { Message = e, Status = MessageTypeEnum.INFO });
            }

        }
    }
}
