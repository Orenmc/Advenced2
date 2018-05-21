using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure;
using Newtonsoft.Json;

namespace ImageService
{
    class GetAppConfigCommand : ICommand
    {
        public string Execute(string[] args, out bool result, out MessageTypeEnum type)
        {

            string[] settings = new string[5];
            settings[0] = ConfigurationManager.AppSettings.Get("OutputDir");
            settings[1] = ConfigurationManager.AppSettings.Get("SourceName");
            settings[2] = ConfigurationManager.AppSettings.Get("LogName");
            settings[3] = ConfigurationManager.AppSettings.Get("ThumbnailSize");
            settings[4] = ConfigurationManager.AppSettings.Get("Handler");

            result = true;
            type = MessageTypeEnum.INFO;
            CommandRecievedEventArgs returnCommand = new CommandRecievedEventArgs((int)CommandStateEnum.GET_APP_CONFIG, settings,null);
            string retCommnad = JsonConvert.SerializeObject(returnCommand);
            return retCommnad;
        }
    }
}
