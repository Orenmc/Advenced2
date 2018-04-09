using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text.RegularExpressions;
using System.Configuration;





namespace ImageService
{
    class ImageModel : IImageModel
    {
        #region Members
        private string m_OutputFolder;            // The Output Folder
        private int m_thumbnailSize;              // The Size Of The Thumbnail Size

        #endregion

        public ImageModel()
        {
            m_OutputFolder = ConfigurationManager.AppSettings.Get("OutputDir");
            //m_thumbnailSize = Int32.Parse(ConfigurationManager.AppSettings.Get("ThumbnailSize"));
        }

        public string AddFile(string path, out bool result)
        {
            string newPath;
            result = true;
            try
            {
                CreateOutputDir(); //create output dir (m_OutputFolder) if not exist.

                DateTime imageCreate = GetDateTakenFromImage(path); //takes the original creatition date of the image

                string year = imageCreate.Year.ToString();
                string month = imageCreate.Month.ToString();
                newPath = m_OutputFolder + "\\" + year + "\\" + month;

                Directory.CreateDirectory(path: m_OutputFolder + "\\" + year);
                Directory.CreateDirectory(path: m_OutputFolder + "\\" + year + "\\" + month);



                //File.Move("C:\\Snir\\orenId.PNG", "C:\\Snir\\test\\2018\\3");
                string file_name = Path.GetFileName(path);
                string file_name_without = Path.GetFileNameWithoutExtension(path);
                string extention = Path.GetExtension(path);
                string test =Path.Combine(newPath ,file_name);
                int counter = 0;
                while (File.Exists(test))
                {
                    counter++;
                   // Console.WriteLine(test);
                    test = Path.Combine(newPath,file_name_without , counter.ToString(), extention);
                }
               File.Move(path, test);

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
