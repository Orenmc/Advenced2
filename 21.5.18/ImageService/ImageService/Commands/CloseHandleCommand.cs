using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure;
using Newtonsoft.Json;

namespace ImageService
{
    /// <summary>
    /// Close handle for directory
    /// </summary>
    class CloseHandleCommand : ICommand
    {
        /// <summary>
        /// not relevant - must not execute.!
        /// </summary>
        public string Execute(string[] args, out bool result, out MessageTypeEnum type)
        {
            result = true;
            type = MessageTypeEnum.INFO;
            CommandRecievedEventArgs returnCommand = new CommandRecievedEventArgs((int)CommandStateEnum.CLOSE_HANDLER, args, null);
            string retCommnad = JsonConvert.SerializeObject(returnCommand);
            return retCommnad;
        }
    }
}
