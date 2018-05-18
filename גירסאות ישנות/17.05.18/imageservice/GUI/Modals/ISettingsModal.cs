using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI
{
	interface ISettingsModal: INotifyPropertyChanged
	{
		ObservableCollection<string> HandlerList { get; set; }
		string OutputDir { get; set; }
		string SourceName { get; set; }
		string LogName { get; set; }
		string ThumbnailSize { get; set; }
	}
}
