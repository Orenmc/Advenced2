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
        private IImageController c_controller;
        private ILoggingModel m_logging;
        #endregion

        #region Properties
        public event EventHandler<CommandRecievedEventArgs> CommandRecieved;          // The event that notifies about a new Command being recieved
        public event EventHandler<DirectoryCloseEventArgs> DirectoryClose;

        #endregion

        public ImageServer(IImageController controller, ILoggingModel logger)
        {
            c_controller = controller;
            m_logging = logger;

            string[] dir = (ConfigurationManager.AppSettings.Get("Handler").Split(';'));

            foreach (string path in dir){
                CreateHandler(path);
            }
        }

        private void CreateHandler(string path)
        {
            IDirectoryHandler d_handle = new DirectoyHandler(m_logging,c_controller,path);
            CommandRecieved += d_handle.OnCommandRecieved;
            DirectoryClose += d_handle.StopHandleDirectory;

        }
        public void OnCloseServer()
        {
            m_logging.Log("close Service", MessageTypeEnum.INFO);
            Console.WriteLine("on close service - remove evemts");

            DirectoryClose?.Invoke(this, null);
        }
        private void SendCommand(CommandRecievedEventArgs e)
        {
            //CommandRecieved.Invoke(this, e);
            Console.WriteLine("Image Server. send Command - WTF???");
        }
    }
}
