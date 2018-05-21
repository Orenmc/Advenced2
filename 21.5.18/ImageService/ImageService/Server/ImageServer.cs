using System;
using System.Configuration;
using System.IO;
using Infrastructure;

namespace ImageService
{
    /// <summary>
    /// Image server class
    /// </summary>
    class ImageServer
    {
        #region Members
        private IImageController c_controller;
        private ILoggingModel m_logging;
        #endregion

        #region Properties
        /// <summary>
        /// The event that notifies about a new Command being recieved
        /// </summary>
        public event EventHandler<CommandRecievedEventArgs> CommandRecieved;
        #endregion

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="controller"> Image controller</param>
        /// <param name="logger">Image logger</param>
        public ImageServer(IImageController controller, ILoggingModel logger)
        {
            c_controller = controller;
            m_logging = logger;
            //gets all paths of directory that needed to be handeled
            string[] dir = (ConfigurationManager.AppSettings.Get("Handler").Split(';'));
            

            foreach (string path in dir){
                if (Directory.Exists(path))
                {
                    CreateHandler(path);

                } else
                {
                    m_logging.Log(path + " not exist - not handle this Dir", MessageTypeEnum.FAIL);
                }
            }

        }
        /// <summary>
        /// create handler on specific Folder - and add it to CommandRecived event
        /// </summary>
        /// <param name="path">path to Folder</param>
        private void CreateHandler(string path)
        {
            m_logging.Log("Start handle Dir: " + path, MessageTypeEnum.INFO);
            IDirectoryHandler d_handle = new DirectoyHandler(m_logging,c_controller,path);
            CommandRecieved += d_handle.OnCommandRecieved;
            d_handle.DirectoryClose += CloseDirectory;
        }
        /// <summary>
        /// send the command to all folders that register to the commandRecived event. 
        /// </summary>
        /// <param name="e"></param>
        public void SendCommand(CommandRecievedEventArgs e)
        {
            CommandRecieved?.Invoke(this, e);
        }
        /// <summary>
        /// remove the directory handle from the CommanedRecived event
        /// </summary>
        /// <param name="sender"> sender class</param>
        /// <param name="e"> arguments of Directory close</param>
        public void CloseDirectory(object sender, DirectoryCloseEventArgs e)
        {
            IDirectoryHandler d = (DirectoyHandler)sender;
            CommandRecieved -= d.OnCommandRecieved;
            d.DirectoryClose -= CloseDirectory;
            m_logging.Log(e.Message , MessageTypeEnum.INFO);
        }
    }
}