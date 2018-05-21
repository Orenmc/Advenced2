using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Data;
using Infrastructure;
using Newtonsoft.Json;

namespace GUI
{
    class LogModel : ILogModal
    {
        private GuiClient client;
        public ObservableCollection<MessageRecievedEventArgs> LogList { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        public LogModel()
        {
            client = GuiClient.GetInstance;
            client.CommandRecieved += this.OnUpdate;
            InitializedLog();
        }

        
        private void InitializedLog()
        {
            LogList = new ObservableCollection<MessageRecievedEventArgs>();
            Object thisLock = new Object();
            BindingOperations.EnableCollectionSynchronization(LogList, thisLock);
            CommandRecievedEventArgs commanToSent = new CommandRecievedEventArgs((int)CommandStateEnum.GET_ALL_LOG, new string[5], "");
            client.SendCommandToServer(commanToSent);

          
        }

        private void OnUpdate(object src, CommandRecievedEventArgs e)
        {
            if (e.CommandID == (int)CommandStateEnum.GET_ALL_LOG)
                this.AllLogArraived(e);
            else if (e.CommandID == (int)CommandStateEnum.GET_NEW_LOG)
                this.NewLogArraived(e);
            else if(e.CommandID == (int)CommandStateEnum.CLOSE)
            {
                new Task(() =>
                {
                    MessageRecievedEventArgs serviceClose = new MessageRecievedEventArgs
                    {
                        Message = "Connection failed - Service was stopped!",
                        Status = MessageTypeEnum.FAIL
                    };
                    Thread.Sleep(millisecondsTimeout: 2000);
                    this.LogList.Add(serviceClose);
                }).Start();
            }
        }

        private void NewLogArraived(CommandRecievedEventArgs e)
        {
            MessageRecievedEventArgs newLog = new MessageRecievedEventArgs
            {
                Message = e.Args[0]
            };
            int num = Int32.Parse(e.Args[1]);
            switch(num)
            {
                case 0:
                    newLog.Status = MessageTypeEnum.INFO;
                    break;
                case 1:
                    newLog.Status = MessageTypeEnum.WARNING;
                    break;
                case 2:
                    newLog.Status = MessageTypeEnum.FAIL;
                    break;
            }
            this.LogList.Add(newLog);
 
        }

        private void AllLogArraived(CommandRecievedEventArgs e)
        {
            ObservableCollection<MessageRecievedEventArgs> newOne = JsonConvert.DeserializeObject<ObservableCollection<MessageRecievedEventArgs>>(e.Args[0]);
            foreach (MessageRecievedEventArgs msg in newOne)
            {
                this.LogList.Add(msg);
            }
        }
    }
}
