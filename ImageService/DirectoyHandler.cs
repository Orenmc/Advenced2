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
        List<FileSystemWatcher> watchers;
        private string dirPath;
       // private string[] extends = { ".bmp", ".jpg", ".gif", ".png" };
        string[] filters = { "*.bmp", "*.jpg", "*.gif", "*.png","*.PNG"};

        public DirectoyHandler(ILoggingModel loggingModel, IImageController imageController, string path)
        {
            c_imageController = imageController;
            m_loggingModel = loggingModel;
            dirPath = path;
            watchers = new List<FileSystemWatcher>();
            this.StartHandleDirectory(path);

        }


//        public event EventHandler<DirectoryCloseEventArgs> DirectoryClose;
        

        public void OnCommandRecieved(object sender, CommandRecievedEventArgs e)
        {
            
            if(e.RequestDirPath == dirPath)
            {
                //for new File command taks the args[0].
                string msg = c_imageController.ExecuteCommand(e.CommandID, e.Args, out bool result);
                if (result)
                {
                    m_loggingModel.Log(msg, MessageTypeEnum.INFO);
                    Console.WriteLine("good -directory handl - result");
                }
                else
                {
                    m_loggingModel.Log(msg, MessageTypeEnum.FAIL);
                    Console.WriteLine("notgood -directory handl - result");
                }
            }
            
        }

        public void StartHandleDirectory(string dirPath)
        {
            m_loggingModel.Log("Start handle " + dirPath, MessageTypeEnum.INFO);
            Console.WriteLine("Directory handle - start listen to this folder " + dirPath);
            /**
            string[] alreadyFilesInDir = Directory.GetFiles(dirPath);
            
            foreach(string image in alreadyFilesInDir)
            {
                if (extends.Contains(Path.GetExtension(image)))
                {
                    string[] args = { image };

                    CommandRecievedEventArgs e = new CommandRecievedEventArgs((int)CommandState.NEW_FILE, args, null);
                    OnCommandRecieved(this, e);
                }
            }
            */
            //create fileSystemWatcher and for all filters { "*.bmp", "*.jpg", "*.gif", "*.png"}
            foreach (string f in filters)
            {
                FileSystemWatcher w = new FileSystemWatcher(this.dirPath);
                w.Filter = f;
                w.Changed += new FileSystemEventHandler(OnImageCreated);
                w.Created += new FileSystemEventHandler(OnImageCreated);
                w.EnableRaisingEvents = true;
                watchers.Add(w);
            }
            
        }

        private void OnImageCreated(object sender,FileSystemEventArgs e)
        {
            Console.WriteLine("entered Directory handler -" + e.FullPath + "is the full path");
            string[] args = { e.FullPath };
            CommandRecievedEventArgs cArgs = new CommandRecievedEventArgs((int)CommandState.NEW_FILE, args, dirPath);
            OnCommandRecieved(this, cArgs);
            
        }
        public void StopHandleDirectory(object sender, DirectoryCloseEventArgs e)
        {
            m_loggingModel.Log("Stop handle " + dirPath, MessageTypeEnum.INFO);
            Console.WriteLine("entered Directory handler -" + dirPath + "is the full path");
            foreach (FileSystemWatcher w in watchers)
            {
                w.EnableRaisingEvents = false;
            }
            ImageServer imageServer = (ImageServer)sender;
            imageServer.CommandRecieved -= OnCommandRecieved;
            imageServer.DirectoryClose -= StopHandleDirectory;
            Console.WriteLine("clear all watchers on directory - directory handle");
            watchers.Clear();
        }
    }
}
