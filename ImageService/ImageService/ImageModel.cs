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
        private string m_ThumFolder;
        private string m_defualtFolder;
        #endregion

        public ImageModel()
        {
            m_OutputFolder = ConfigurationManager.AppSettings.Get("OutputDir");
            m_thumbnailSize = Int32.Parse(ConfigurationManager.AppSettings.Get("ThumbnailSize"));
            m_ThumFolder = Path.Combine(m_OutputFolder, "Thumbnail");
            m_defualtFolder = m_OutputFolder + "\\default";


            CreateOutputDir(); //create output dir (m_OutputFolder) if not exist.

        }

        public string AddFile(string path, out bool result,out MessageTypeEnum type)
        {
            string newPath; //path of the new file
            result = true;
            string realPath;
            try
            {

                DateTime imageCreate = GetDateTakenFromImage(path); //takes the original creatition date of the image

                string year = imageCreate.Year.ToString();
                string month = imageCreate.Month.ToString();
                //return value - string of curren folder

                //TODO: check if need to return the folder or the IMAGE! - if image need to Path.Combine(path,file_name);

                newPath = m_OutputFolder + "\\" + year + "\\" + month;
                //create year folder and year\month in outputDIR (if not exist)
                Directory.CreateDirectory(path: Path.Combine(m_OutputFolder, year));
                Directory.CreateDirectory(path: Path.Combine(m_OutputFolder, year, month));
                //create year folder and year\month in ThumbDIR (if not exist)
                Directory.CreateDirectory(path: Path.Combine(m_ThumFolder, year));
                string thumb_path = Path.Combine(m_ThumFolder, year, month);
                Directory.CreateDirectory(path: thumb_path);
                
                
                if (NamingToNewImage(path, newPath, out realPath))
                {
                    type = MessageTypeEnum.INFO;
                }
                else
                {
                    type = MessageTypeEnum.WARNING;
                }

                File.Move(path, realPath);

                Image image = Image.FromFile(realPath);
                image = (Image)(new Bitmap(image, new Size(m_thumbnailSize, m_thumbnailSize)));
                image.Save(Path.Combine(thumb_path,Path.GetFileName(realPath)));
                
            }
            catch (Exception e)
            {
                result = false;
                type = MessageTypeEnum.FAIL;
                //TODO: test here - remove consoleWrite
                Console.WriteLine(e.Message);
                //TODO - need to catch this! - something withthreads... its pronts to screan

                File.Move(path, m_defualtFolder +"\\"+ Path.GetFileName(path));

                return "problem with file " + e.Message +" " +  e.GetType().ToString();
            }
            if(type == MessageTypeEnum.INFO)
            {
                return "AddedFile " + Path.GetFileName(path) + " to: " + realPath;
            }
            else
            {
                return "AlreadyExist - name changed AddFile " + Path.GetFileName(path) + " to: " + realPath;

            }

        }
        private bool NamingToNewImage(string imagePath, string newPath, out string realPath)
        {
            
            int counter = 0;    //counter for add "(counter)" to file if already exist
            
            string file_name = Path.GetFileName(imagePath);     //file name e.g : oren.txt
            string file_name_without = Path.GetFileNameWithoutExtension(imagePath); //file without extenetion e.g: oren
            string extention = Path.GetExtension(imagePath);    //file extention e.g: .txt (with the DOT!)
            realPath = Path.Combine(newPath, file_name);     //comabin- e.g:from - .../output/2018/4 and oren.txt, to- .../output/2018/4/oren.txt 
            
            //if exsist increment counter and try again
            while (File.Exists(realPath))
            {
                counter++;
                realPath = Path.Combine(newPath, file_name_without + "(" + counter.ToString() + ")" + extention);
            }

            return (counter == 0);
            
        }

        private void CreateOutputDir()
        {
            //create output directory
            DirectoryInfo di_output = Directory.CreateDirectory(path: m_OutputFolder);
            //make the directory hidden
            di_output.Attributes = FileAttributes.Directory | FileAttributes.Hidden;
            //create thumbnail directory
            DirectoryInfo di_thum = Directory.CreateDirectory(path: m_ThumFolder);

            Directory.CreateDirectory(path: m_OutputFolder + "\\default");
            
            //TODO: check if need to be hidden
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
