using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure;

namespace ImageService
{
    class AllLogCommand : ICommand
    {
        private ILoggingModel log_Modal;
        public AllLogCommand(ILoggingModel logModal)
        {
            this.log_Modal = logModal;
        }
        public string Execute(string[] args, out bool result, out MessageTypeEnum type)
        {
            ObservableCollection<MessageRecievedEventArgs> logEntry = this.log_Modal.LogMsg;
            int x = logEntry.Count;
            StringBuilder sb = new StringBuilder();
            sb.Append(logEntry[x-1].Message).Append(".").Append("success").Append(".");
            sb.Append(logEntry[x-2].Message).Append(".").Append("Worning");

            result = true;
            type = MessageTypeEnum.INFO;

            return sb.ToString();
        }
    }
}
