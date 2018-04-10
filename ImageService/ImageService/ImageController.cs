using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public enum CommandState : int
{
    NEW_FILE,
    CLOSE,
}

namespace ImageService
{
    class ImageController : IImageController
    {
        private Dictionary<int, ICommand> commands;
        private IImageModel m_imageModel;

        public ImageController(IImageModel imageModel)
        {
            
            this.commands = new Dictionary<int, ICommand>();
            this.m_imageModel = imageModel;

            //add new file - to command Dictionary
            commands[(int)CommandState.NEW_FILE] = new NewFileCommand(m_imageModel);

            //close handlers
            commands[(int)CommandState.CLOSE] = new CloseHandleCommand();  

        }

        public string ExecuteCommand(int commandID, string[] args, out bool result, out MessageTypeEnum type)
        {
           Console.WriteLine("image controller - dont forget to do this in tasks");
           return commands[commandID].Execute(args, out result,out type);
        }
    }
}
