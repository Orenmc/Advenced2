using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService
{
    /// <summary>
    /// Close handle for directory
    /// </summary>
    class CloseHandleCommand : ICommand
    {
        /// <summary>
        /// not relevant - must not execute.!
        /// </summary>
        public string Execute(string[] args, out bool result, out MessageTypeEnum type)
        {
            throw new NotImplementedException();
        }
    }
}
