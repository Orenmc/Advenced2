using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService
{
    class ImageModel : IImageModel
    {
        #region Members
        private string m_OutputFolder;            // The Output Folder
        private int m_thumbnailSize;              // The Size Of The Thumbnail Size

        #endregion

        public string AddFile(string path, out bool result)
        {
            string msg = "all good";
            result = true;
            try
            {
                Directory.CreateDirectory(path: m_OutputFolder);
                DateTime create = File.GetCreationTime(path);
                string year = create.Year.ToString();
                string month = create.Month.ToString();
                string newPath = m_OutputFolder + "\"" + year + "\"" + month;
                //System.IO.Directory.CreateDirectory(m_OutputFolder+ "\"" + year);
                Directory.CreateDirectory(path: newPath);
                System.IO.File.Copy(path, newPath);
            }
            catch (Exception e)
            {
                msg = e.Message;
                result = false;
            }
            
            

            return msg;
        }
    }
}
