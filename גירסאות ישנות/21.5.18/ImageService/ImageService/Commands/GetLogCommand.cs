using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure;
using Newtonsoft.Json;

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
            string jsonLog = JsonConvert.SerializeObject(logEntry);
            string[] argument = new string[1];
            argument[0] = jsonLog;
            CommandRecievedEventArgs returnCommand = new CommandRecievedEventArgs((int)CommandStateEnum.GET_ALL_LOG, argument, "");
            string retCommand = JsonConvert.SerializeObject(returnCommand);
            result = true;
            type = MessageTypeEnum.INFO;

            return retCommand;
            
            
        }
    }
}
