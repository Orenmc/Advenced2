using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure;

namespace ImageService
{
    class GetAppConfigCommand : ICommand
    {
        public string Execute(string[] args, out bool result, out MessageTypeEnum type)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(ConfigurationManager.AppSettings.Get("OutputDir")).Append(".");
            sb.Append(ConfigurationManager.AppSettings.Get("SourceName")).Append(".");
            sb.Append(ConfigurationManager.AppSettings.Get("LogName")).Append(".");
            sb.Append(ConfigurationManager.AppSettings.Get("ThumbnailSize")).Append(".");
            sb.Append(ConfigurationManager.AppSettings.Get("Handler"));

            result = true;
            type = MessageTypeEnum.INFO;
            return sb.ToString();

        }
    }
}
