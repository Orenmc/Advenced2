using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI
{
	class MainWindowModal : IMaimWindowModal
	{
		public event PropertyChangedEventHandler PropertyChanged;
		private bool connected;

		public MainWindowModal()
		{
			this.connected = false;
		}

		public bool Connected
		{
			get => connected;
			set
			{
				if (connected != value)
				{
					connected = value;
					NotifyPropertyChanged("Connected");

				}
			}
		}

		public void NotifyPropertyChanged(string propName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
		}
	}
}
