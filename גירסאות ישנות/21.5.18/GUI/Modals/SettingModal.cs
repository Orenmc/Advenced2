using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Infrastructure;
using Newtonsoft.Json;

namespace GUI
{
	class SettingModal : ISettingsModal
	{
		private string outputDir;
		private string sourceName;
		private string logName;
		private string thumbnailSize;
        private GuiClient client;
		public event PropertyChangedEventHandler PropertyChanged;

		public SettingModal()
		{
            HandlerList = new ObservableCollection<string>();
            client = GuiClient.GetInstance;
            client.CommandRecieved += this.OnUpdate;
            client.RecivedMessageFromServer();
            InitializedSettings();
                 
        }

        private void OnUpdate(object src, CommandRecievedEventArgs e)
        {
            if (e.CommandID == (int)CommandStateEnum.GET_APP_CONFIG)
            {
                NewConfiguration(e);
            } else if (e.CommandID == (int)CommandStateEnum.CLOSE_HANDLER)
            {
                this.HandlerList.Remove(e.Args[0]);
            }
        }

        private void NewConfiguration(CommandRecievedEventArgs e)
        {
            OutputDir = e.Args[0];
            SourceName = e.Args[1];
            LogName = e.Args[2];
            ThumbnailSize = e.Args[3];
            string[] handler = e.Args[4].Split(';');

            foreach (string handle in handler)
            {
                HandlerList.Add(handle);
            }
        }

        private void InitializedSettings()
        {
            Object thisLock = new Object(); 
            BindingOperations.EnableCollectionSynchronization(HandlerList, thisLock);
            CommandRecievedEventArgs commanToSent = new CommandRecievedEventArgs((int)CommandStateEnum.GET_APP_CONFIG, new string[5], "");
            client.SendCommandToServer(commanToSent);

        }

        public void NotifyPropertyChanged(string propName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
		}

		public string OutputDir
		{
			get => outputDir;
			set {
				if (outputDir != value)
				{
					outputDir = value;
					NotifyPropertyChanged("OutputDir");
				}
			}
		}
		public string SourceName
		{
			get => sourceName;
			set
			{
				if (sourceName != value)
				{
					sourceName = value;
					NotifyPropertyChanged("SourceName");
				}
			}
		}
		public string LogName
		{
			get => logName;
			set
			{
				if (logName != value)
				{
					logName = value;
					NotifyPropertyChanged("LogName");
				}
			}
		}

		public string ThumbnailSize
		{
			get => thumbnailSize;
			set
			{
				if (thumbnailSize != value)
				{
					thumbnailSize = value;
					NotifyPropertyChanged("ThumbnailSize");
				}
			}
		}

        public ObservableCollection<string> HandlerList { get; set; }
    }
}
