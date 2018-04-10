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
        string[] filters = { "*.bmp", "*.jpg", "*.gif", "*.png"};

        public DirectoyHandler(ILoggingModel loggingModel, IImageController imageController, string path)
        {
            c_imageController = imageController;
            m_loggingModel = loggingModel;
            dirPath = path;
            watchers = new List<FileSystemWatcher>();
            this.StartHandleDirectory(path);

        }


        public event EventHandler<DirectoryCloseEventArgs> DirectoryClose;
        

        public void OnCommandRecieved(object sender, CommandRecievedEventArgs e)
        {
            
            if(e.RequestDirPath == dirPath || e.RequestDirPath == "*")
            {
                if (e.CommandID == (int)CommandState.CLOSE)
                {
                    DirectoryCloseEventArgs e1 = new DirectoryCloseEventArgs(dirPath, "Stop handle directory: " + dirPath);
                    StopHandleDirectory();
                    DirectoryClose.Invoke(this, e1);
                }
                else
                {
                    //for new File command taks the args[0].
                    string msg = c_imageController.ExecuteCommand(e.CommandID, e.Args, out bool result , out MessageTypeEnum type);

                    //TODO: check that the its written in the log!!!!
                    if (result)
                    {
                        m_loggingModel.Log(msg, type);
                    }
                    else
                    {
                        m_loggingModel.Log(msg, MessageTypeEnum.FAIL);
                    }
                }
            }
            
        }

        public void StartHandleDirectory(string dirPath)
        {
            //DirectoryClose += StopHandleDirectory; 

           // m_loggingModel.Log("Start handle " + dirPath, MessageTypeEnum.INFO);
           //TODO: no need to log this - its to early (before in start)


            //create fileSystemWatcher and for all filters { "*.bmp", "*.jpg", "*.gif", "*.png"}
            foreach (string f in filters)
            {
                FileSystemWatcher w = new FileSystemWatcher(this.dirPath);
                w.Filter = f;
               // w.Changed += new FileSystemEventHandler(OnImageCreated);
                w.Created += new FileSystemEventHandler(OnImageCreated);
                w.EnableRaisingEvents = true;
                watchers.Add(w);
            }
            
        }

        private void OnImageCreated(object sender,FileSystemEventArgs e)
        {
            //Console.WriteLine("entered Directory handler -" + e.FullPath + "is the full path");
            string[] args = { e.FullPath };
            CommandRecievedEventArgs cArgs = new CommandRecievedEventArgs((int)CommandState.NEW_FILE, args, dirPath);
            OnCommandRecieved(this, cArgs);
            
        }
        public void StopHandleDirectory()
        {

            foreach (FileSystemWatcher w in watchers)
            {
                w.EnableRaisingEvents = false;
            }
            watchers.Clear();
        }
    }
}
