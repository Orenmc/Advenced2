using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GUI
{
	class SettingsViewModal : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		private ISettingsModal modal;
		private string selected;

		public SettingsViewModal(ISettingsModal modal)
		{
			this.modal = modal;
			this.modal.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e)
			{
				NotifyPropertyChanged("VM_" + e.PropertyName);
			};

		}

		public void NotifyPropertyChanged(string propName)
		{
			this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
		}

		public void ChangedPropertyTest()
		{
			this.modal.ThumbnailSize = "stringTest";
			this.VM_HandlerList.Remove(this.selected);
			Console.WriteLine(this.selected);

		}


		public ObservableCollection<string> VM_HandlerList
		{
			get => this.modal.HandlerList;
			set => throw new NotImplementedException();
		}
		public string VM_OutputDir
		{
			get => this.modal.OutputDir;
		}
		public string VM_SourceName
		{
			get => modal.SourceName;
		}
		public string VM_LogName
		{
			get => modal.LogName;
		}
		public string VM_ThumbnailSize
		{
			get => modal.ThumbnailSize;
		}
		public string Selected
		{
			get => this.selected;

			set
			{
				selected = value;
			}
		}


	}
}
