using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService
{
    interface IImageController
    {
        string ExecuteCommand(int commandID, string[] args, out bool result,out MessageTypeEnum type);// Executing the Command Requet
    }
}
