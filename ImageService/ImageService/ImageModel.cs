using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text.RegularExpressions;





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
                CreateOutputDir(); //create output dir (m_OutputFolder) if not exist.

                DateTime imageCreate = GetDateTakenFromImage(path); //takes the original creatition date of the image

                newPath = CreateFolder(imageCreate); //create Folder with the year and day path - return the new path as string
                File.Move(sourceFileName: path, destFileName: newPath); // move image to the new path
            }
            catch (Exception e)
            {
                result = false;
                return e.Message;
            }
            return "Added image " + Path.GetFileName(path) + "to " + newPath;
        }
    
        private void CreateOutputDir()
        {
            DirectoryInfo di = Directory.CreateDirectory(path: m_OutputFolder);
           // Console.WriteLine("do the directory hidden - Image model");
            di.Attributes = FileAttributes.Directory | FileAttributes.Hidden;
        }
        private string CreateFolder(DateTime dateImage)
        {
            string year = dateImage.Year.ToString();
            string month = dateImage.Month.ToString();
            string newPath = m_OutputFolder + "\\" + year + "\\" + month;
            Directory.CreateDirectory(path: newPath);

            return newPath;
        }
        //we init this once so that if the function is repeatedly called
        //it isn't stressing the garbage man
        private static Regex r = new Regex(":");

        public static DateTime GetDateTakenFromImage(string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            using (Image myImage = Image.FromStream(fs, false, false))
            {
                PropertyItem propItem = myImage.GetPropertyItem(36867);
                string dateTaken = r.Replace(Encoding.UTF8.GetString(propItem.Value), "-", 2);
                return DateTime.Parse(dateTaken);
            }
        }


    }
}
