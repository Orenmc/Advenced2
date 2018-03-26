using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

enum MessageTypeEnum : int {INFO, WARNING, FAIL}

namespace ImageService
{
    class LoggingModel : ILoggingModel
    {
        public event EventHandler<MessageRecievedEventArgs> MessageRecieved;
        public void Log(string message, MessageTypeEnum type)
        {
            MessageRecievedEventArgs msg = new MessageRecievedEventArgs();
            msg.Message = message;
            msg.Status = type;

            MessageRecieved.Invoke(this, msg);

        }
    }
}