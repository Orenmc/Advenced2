using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService
{
    class AppSettingValue
    {
        private static string sourceName;
        private static string logName;
        private static string outputDir;
        private static string thumbnailSize;
        private static string handlers;

        public static string SourceName
        {
            get
            {
                if(sourceName == null)
                {
                    sourceName = ConfigurationManager.AppSettings.Get("SourceName");
                }
                return sourceName;
            }
            set
            {
                sourceName = value;
            }
        }
        public static string OutputDir
        {
            get
            {
                if (outputDir == null)
                {
                    outputDir = ConfigurationManager.AppSettings.Get("OutputDir");

                }
                return outputDir;
            }
            set
            {
                outputDir = value;
            }
        }

        public static string LogName
        {
            get
            {
                if (logName == null)
                {
                    logName = ConfigurationManager.AppSettings.Get("LogName");

                }
                return logName;
            }
            set
            {
                logName = value;
            }
        }
        public static string ThumbnailSize
        {
            get
            {
                if (thumbnailSize == null)
                {
                    thumbnailSize = ConfigurationManager.AppSettings.Get("ThumbnailSize");

                }
                return thumbnailSize;
            }
            set
            {
                thumbnailSize = value;
            }
        }
        public static string Handlers
        {
            get
            {
                if (handlers == null)
                {
                    handlers = ConfigurationManager.AppSettings.Get("Handler");
                }
                return handlers;
            }
            set
            {
                handlers = value;
            }
        }
    }
}
