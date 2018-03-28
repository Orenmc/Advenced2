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
            string newPath;
            result = true;
            try
            {
                DirectoryInfo di = Directory.CreateDirectory(path: m_OutputFolder);
                Console.WriteLine("do the directory hidden - Image model");
                //di.Attributes = FileAttributes.Directory | FileAttributes.Hidden;
                DateTime create = File.GetCreationTime(path);
                string year = create.Year.ToString();
                string month = create.Month.ToString();
                newPath = m_OutputFolder + "\\" + year + "\\" + month;
                //System.IO.Directory.CreateDirectory(m_OutputFolder+ "\"" + year);
                Directory.CreateDirectory(path: newPath);
                File.Copy(sourceFileName: path, destFileName: newPath);
            }
            catch (Exception e)
            {
                result = false;
                return e.Message;
            }
            return newPath;
        }
    }
}
