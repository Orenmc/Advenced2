using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ImageService
{
    class DirectoyHandler : IDirectoryHandler
    {
        private IImageController c_imageController;
        private ILoggingModel m_loggingModel;
        private FileSystemWatcher fileSystemWatcher;
        private string dirPath;
        private string[] extends = { "*.bmp", "*.jpg", "*.gif", "*.png" };

        public DirectoyHandler(ILoggingModel loggingModel, IImageController imageController, string path)
        {
            c_imageController = imageController;
            m_loggingModel = loggingModel;
            dirPath = path;
            FolderFilter(); //create filesystemwatcher and for now only one filter (jpg)
            
            

           // this.StartHandleDirectory(path);
        }

        public event EventHandler<DirectoryCloseEventArgs> DirectoryClose;
        

        public void OnCommandRecieved(object sender, CommandRecievedEventArgs e)
        {
            string[] s = { e.RequestDirPath };
            c_imageController.ExecuteCommand(e.CommandID, s, out bool result);
            if(result)
            {
                Console.WriteLine("good -directory handl - result");
            } else
            {
                Console.WriteLine("notgood -directory handl - result");
            }
        }

        public void StartHandleDirectory(string dirPath)
        {
            CommandRecievedEventArgs e = new CommandRecievedEventArgs((int)CommandState.NEW_FILE, null, dirPath);
            //OnCommandRecieved(this, recievedEventArgs);
        }
        private void FolderFilter()
        {
            fileSystemWatcher = new FileSystemWatcher(dirPath,"*.jpg");

        }
    }
}
