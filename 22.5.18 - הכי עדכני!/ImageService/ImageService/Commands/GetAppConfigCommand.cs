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
            settings[0] = AppSettingValue.OutputDir;
            settings[1] = AppSettingValue.SourceName;
            settings[2] = AppSettingValue.LogName;
            settings[3] = AppSettingValue.ThumbnailSize;
            settings[4] = AppSettingValue.Handlers;

            result = true;
            type = MessageTypeEnum.INFO;
            CommandRecievedEventArgs returnCommand = new CommandRecievedEventArgs((int)CommandStateEnum.GET_APP_CONFIG, settings,null);
            string retCommnad = JsonConvert.SerializeObject(returnCommand);
            return retCommnad;
        }
    }
}
