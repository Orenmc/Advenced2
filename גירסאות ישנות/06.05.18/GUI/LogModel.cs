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

        private ObservableCollection<MessageRecievedEventArgs> logList;
        public ObservableCollection<MessageRecievedEventArgs> LogList { get =>  this.logList ; set => throw new NotImplementedException(); }


        public event PropertyChangedEventHandler PropertyChanged;

        public LogModel()
        {
            logList = new ObservableCollection<MessageRecievedEventArgs>
            {
                new MessageRecievedEventArgs() { Message = "bla1", Status = MessageTypeEnum.INFO },
                new MessageRecievedEventArgs() { Message = "bla2", Status = MessageTypeEnum.FAIL },
                new MessageRecievedEventArgs() { Message = "bla3", Status = MessageTypeEnum.WARNING },
                new MessageRecievedEventArgs() { Message = "bla4", Status = MessageTypeEnum.WARNING }


            };

        }
    }
}
