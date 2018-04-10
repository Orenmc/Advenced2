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
            d_handle.DirectoryClose += CloseDirectory;
        }

        public void OnCloseServer()
        {
            m_logging.Log("StartCloseService", MessageTypeEnum.INFO);
            CommandRecievedEventArgs e = new CommandRecievedEventArgs((int)CommandState.CLOSE, null, "*");
            CommandRecieved.Invoke(this, e);
        }

        public void CloseDirectory(object sender, DirectoryCloseEventArgs e)
        {
            IDirectoryHandler d = (DirectoyHandler)sender;
            CommandRecieved -= d.OnCommandRecieved;
            d.DirectoryClose -= CloseDirectory;
            //TODO: print path of directory
            m_logging.Log(e.Message , MessageTypeEnum.INFO);
        }
    }
}
