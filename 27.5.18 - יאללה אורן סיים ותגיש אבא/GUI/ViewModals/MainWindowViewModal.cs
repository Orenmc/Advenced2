using System.ComponentModel;

namespace GUI
{
	class MainWindowViewModal : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		private IMaimWindowModal model;
		public MainWindowViewModal(IMaimWindowModal Mymodal)
		{
			model = Mymodal;
			model.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e)
			{
				NotifyPropertyChanged("VM_" + e.PropertyName);
			};
		}

		public void NotifyPropertyChanged(string propName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
		}

		public bool VM_Connected
		{
			get => model.Connected;
		}
	}
}
