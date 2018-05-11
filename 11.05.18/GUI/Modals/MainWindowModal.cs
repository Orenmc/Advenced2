﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

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
           // this.connected = false;

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