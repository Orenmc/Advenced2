using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService
{
    class NewFileCommand : ICommand
    {
        private IImageModel m_model;

        public NewFileCommand(IImageModel modal)
        {
            m_model = modal;            // Storing the Modal
        }

        public string Execute(string[] args, out bool result)
        {
            return m_model.AddFile(args[0], out result);
            // The String Will Return the New Path if result = true, and will return the error message

        }
    }
}
