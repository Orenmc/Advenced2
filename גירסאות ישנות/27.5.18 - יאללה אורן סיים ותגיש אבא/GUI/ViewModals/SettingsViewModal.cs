using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Prism.Commands;
using System.Windows.Input;
using Infrastructure;

namespace GUI
{
    class SettingsViewModal : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		private ISettingsModal modal;
		private string selected;
        private GuiClient client;

        public ICommand RemoveHandlerCommand { get; private set; }

        public SettingsViewModal(ISettingsModal modal)
		{
			this.modal = modal;
            this.client = GuiClient.GetInstance;
			this.modal.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e)
			{
				NotifyPropertyChanged("VM_" + e.PropertyName);
			};
            RemoveHandlerCommand = new DelegateCommand<object>(OnRemoveHandler, CanExecute);
        }

        public void NotifyPropertyChanged(string propName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
		}

		public ObservableCollection<string> VM_HandlerList
		{
			get => modal.HandlerList;
			set => throw new NotImplementedException();
		}

		public string VM_OutputDir
		{
			get => modal.OutputDir;
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
			get => selected;

			set
			{
				selected = value;
                var command = RemoveHandlerCommand as DelegateCommand<object>;
                command.RaiseCanExecuteChanged();
            }
		}
        
        private void OnRemoveHandler(object obj)
        {
            string[] argument = new string[1];
            argument[0] = Selected;
            CommandRecievedEventArgs sendCommand = new CommandRecievedEventArgs((int)CommandStateEnum.CLOSE_HANDLER, argument,"");
            client.SendCommandToServer(sendCommand);
        }

        private bool CanExecute(object obj)
        {
            if (Selected != null)
                return true;
            return false;
        }
    }
}
