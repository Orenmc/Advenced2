using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure;

//enum MessageTypeEnum : int {INFO, WARNING, FAIL}

namespace ImageService
{
    /// <summary>
    /// Logging model
    /// </summary>
    class LoggingModel : ILoggingModel
    {
        /// <summary>
        /// The event that notifies about a new message being recieved
        /// </summary>
        public event EventHandler<MessageRecievedEventArgs> MessageRecieved;
        /// <summary>
        /// write msg to Log event
        /// </summary>
        /// <param name="message">msg to write</param>
        /// <param name="type"> <see cref="MessageTypeEnum"/></param>
        public void Log(string message, MessageTypeEnum type)
        {
            MessageRecievedEventArgs msg = new MessageRecievedEventArgs
            {
                Message = message,
                Status = type
            };
            MessageRecieved?.Invoke(this, msg);
        }
    }
}