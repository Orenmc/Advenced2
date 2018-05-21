using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure;

namespace ImageService
{
    /// <summary>
    /// ICommand Interface
    /// </summary>
    interface ICommand
    {
        /// <summary>
        /// execute command.
        /// </summary>
        /// <param name="args"> arguments for command</param>
        /// <param name="result">true if success, otherwise false</param>
        /// <param name="type">{info,warning,fail} according to execute</param>
        /// <returns>path if success, else error messege  </returns>
        string Execute(string[] args, out bool result, out MessageTypeEnum type);          // The Function That will Execute The 
    }
}
