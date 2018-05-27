using System.ComponentModel;


namespace GUI
{
    class MainWindowModal : IMaimWindowModal
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private bool connected;
        private GuiClient client;

        public MainWindowModal()
        {
            client = GuiClient.GetInstance;
            this.connected = client.IsConnected;

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
