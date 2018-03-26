﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ImageService
{

    interface ILoggingModel
    {

       event EventHandler<MessageRecievedEventArgs> MessageRecieved;
       void Log(string message, MessageTypeEnum type);
    }
}
