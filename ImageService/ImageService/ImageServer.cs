using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService
{
    class ImageServer
    {
        #region Members
        private IImageController m_controller;
        private ILoggingModel m_logging;
        #endregion

        #region Properties
        public event EventHandler<CommandRecievedEventArgs> CommandRecieved;          // The event that notifies about a new Command being recieved
        #endregion

        public ImageServer()
        {

            string[] dir = (ConfigurationSettings.AppSettings.Get("Handler").Split(';'));

            foreach (string path in dir){
                CreateHandler(path);
            }
        }

        private void CreateHandler(string path)
        {
            IDirectoryHandler d_handle = new DirectoyHandler(m_logging,m_controller,path);
            CommandRecieved += d_handle.OnCommandRecieved;
            d_handle.DirectoryClose += OnCloseDir;

        }
        private void OnCloseDir(object sender, DirectoryCloseEventArgs args)
        {
            m_logging.Log(args.Message, MessageTypeEnum.INFO);
            IDirectoryHandler d_handler = (IDirectoryHandler)sender;
            CommandRecieved -= d_handler.OnCommandRecieved;
            //############# how to remove the evenr - Directory Close - in Directory Handler #######33
        }
        private void SendCommand(CommandRecievedEventArgs e)
        {
            //CommandRecieved.Invoke(this, e);
            Console.WriteLine("Image Server. send Command - WTF???");
        }
    }
}
