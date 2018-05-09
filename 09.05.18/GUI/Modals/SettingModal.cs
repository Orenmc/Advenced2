using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI
{
	class SettingModal : ISettingsModal
	{
		private ObservableCollection<string> handlerList;
		private string outputDir;
		private string sourceName;
		private string logName;
		private string thumbnailSize;
		public event PropertyChangedEventHandler PropertyChanged;

		public SettingModal()
		{
			handlerList = new ObservableCollection<string>
			{
				"snir","oren","ABBA","dada","what","gpgpg","shutUp"
			};
			outputDir = "Oren Ahronian";
			sourceName = "sns";
			logName = "sdasdasd";
			thumbnailSize = "121212";
		}

		public void NotifyPropertyChanged(string propName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
		}

		public ObservableCollection<string> HandlerList { get => this.handlerList; set => throw new NotImplementedException(); }

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

	}
}
