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
        private ImageServer severImage;
        public CloseHandleCommand(ImageServer server)
        {
            this.severImage = server;
        }
        /// <summary>
        /// not relevant - must not execute.!
        /// </summary>
        public string Execute(string[] args, out bool result, out MessageTypeEnum type)
        {
            result = true;
            type = MessageTypeEnum.INFO;
            CommandRecievedEventArgs returnCommand = new CommandRecievedEventArgs((int)CommandStateEnum.CLOSE_HANDLER, args, null);
            //remove the handler from the setting Configuration.
            
            string[] handlers = AppSettingValue.Handlers.Split(';');
            StringBuilder sb = new StringBuilder();
            foreach (string path in handlers)
            {
                if (path == args[0])
                    continue;
                sb.Append(path).Append(';');
            }
            if (sb.Length != 0)
                sb.Length--;

            AppSettingValue.Handlers = sb.ToString();
            CommandRecievedEventArgs e = new CommandRecievedEventArgs((int)CommandStateEnum.CLOSE, null, args[0]);

            this.severImage.SendCommand(e);
            

            string retCommnad = JsonConvert.SerializeObject(returnCommand);
            return retCommnad;
        }
    }
}
