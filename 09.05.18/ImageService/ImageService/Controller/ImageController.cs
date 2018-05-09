using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure;

/// <summary>
/// Enum of Command State (to int)
/// </summary>
public enum CommandState : int
{
    NEW_FILE,
    CLOSE,
}

namespace ImageService
{
    /// <summary>
    /// Image Controller
    /// </summary>
    class ImageController : IImageController
    {
        private Dictionary<int, ICommand> commands;
        private IImageModel m_imageModel;

        /// <summary>
        /// cons't
        /// </summary>
        /// <param name="imageModel"> ImageModel (logic of program)</param>
        public ImageController(IImageModel imageModel)
        {
            //Dictionary: key - CommandState , value - command to execute
            this.commands = new Dictionary<int, ICommand>();
            this.m_imageModel = imageModel;

            //add new file - to command Dictionary
            commands[(int)CommandState.NEW_FILE] = new NewFileCommand(m_imageModel);

            //close handlers
            commands[(int)CommandState.CLOSE] = new CloseHandleCommand();  
        }
        /// <summary>
        /// Executing the Command Requet
        /// </summary>
        /// <param name="commandID">CommandId - <see cref="CommandState"/></param>
        /// <param name="args">arguments for the command</param>
        /// <param name="result"> true if succeeded</param>
        /// <param name="type"> <seealso cref="MessageTypeEnum"/></param>
        /// <returns>string - msg of the execute </returns>
        public string ExecuteCommand(int commandID, string[] args, out bool result, out MessageTypeEnum type)
        {
           return commands[commandID].Execute(args, out result,out type);
        }
    }
}
