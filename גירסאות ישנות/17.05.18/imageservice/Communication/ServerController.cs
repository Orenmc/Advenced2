using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure;




namespace Communication
{
    /// <summary>
    /// Image Controller
    /// </summary>
    class ServerController : IImageController
    {
        private Dictionary<int, ICommand> commands;
        private IImageModel m_imageModel;
        private ILoggingModel m_logModal;

        /// <summary>
        /// cons't
        /// </summary>
        /// <param name="imageModel"> ImageModel (logic of program)</param>
        public ServerController(IImageModel imageModel, ILoggingModel logModal)
        {
            //Dictionary: key - CommandState , value - command to execute
            this.commands = new Dictionary<int, ICommand>();
            this.m_imageModel = imageModel;
            this.m_logModal = logModal;

            //add new file - to command Dictionary
            commands[(int)CommandStateEnum.NEW_FILE] = new NewFileCommand(m_imageModel);

            //TODO BOM
            commands[(int)CommandStateEnum.GET_APP_CONFIG] = new GetAppConfigCommand();

            //close handlers
            commands[(int)CommandStateEnum.CLOSE] = new CloseHandleCommand();

            commands[(int)CommandStateEnum.GET_ALL_LOG] = new AllLogCommand(this.m_logModal);

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
