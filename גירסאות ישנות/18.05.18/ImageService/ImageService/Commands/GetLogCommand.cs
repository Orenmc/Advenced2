using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure;

namespace ImageService
{
    class GetLogCommand : ICommand
    {
        private ILoggingModel log_Modal;
        public GetLogCommand(ILoggingModel logModal)
        {
            this.log_Modal = logModal;
        }
        public string Execute(string[] args, out bool result, out MessageTypeEnum type)
        {
            
            ObservableCollection<MessageRecievedEventArgs> logEntry = this.log_Modal.LogMsg;
            int x = logEntry.Count;
            StringBuilder sb = new StringBuilder();
            int i = 0;
            for(; i < x - 1; i++)
            {
                sb.Append(logEntry[i].Message).Append("%");
            }
            sb.Append(logEntry[i].Message);

            result = true;
            type = MessageTypeEnum.INFO;

            return sb.ToString();
            
            
        }
    }
}
