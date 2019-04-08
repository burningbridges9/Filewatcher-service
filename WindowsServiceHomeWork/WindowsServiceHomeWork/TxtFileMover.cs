using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsServiceHomeWork
{
    class TxtFileMover : FileMover
    {
        DirectoryInfo TxtDirInfo { get; }
        /// <summary>
        /// first init 
        /// </summary>
        /// <param name="curPath"></param>
        /// <param name="destPath"></param>
        public TxtFileMover(string curPath, string destPath) : base(curPath, destPath) // ConfigurationManager.AppSettings["XMLDestinationPath"];
        {
            
            TxtDirInfo = new DirectoryInfo(destPath);
            if (!TxtDirInfo.Exists) // if there's not folder 
            {
                TxtDirInfo.Create();
            }
        }

        /// <summary>
        /// search .txt files and move em to dest path
        /// </summary>
        /// <param name="file"></param>
        public override void Move(FileInfo file)//getting file that will be moved
        {   //checking files in destination pathe
            int counter = 0;// counter for the same files 
            foreach (FileInfo fileSub in TxtDirInfo.GetFiles(file.Extension)) //search pattern
            {

                if (fileSub.Name.Contains(file.Name))
                {
                    if (fileSub.Name.LastIndexOf(".") == file.Name.LastIndexOf("."))
                        counter++;
                    else
                    {

                        string potentialNumber = fileSub.Name.Substring(file.Name.LastIndexOf("."), fileSub.Name.LastIndexOf("."));
                        int number = Int32.Parse(potentialNumber);
                        if (Int32.TryParse(potentialNumber, out number))
                        {
                            counter++;
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
            }

            if (counter == 0)
            {
                string newName = file.Name.Substring(0, file.Name.LastIndexOf(".")) + file.Extension;
                file.MoveTo(DestinationPath + "\\" + newName);
                Logger.Log.Info("Перемещен файл: " + file.Name + " в папку: " + DestinationPath + "имя файла в папке: " + newName);
            }
            else
            {
                string newName = file.Name.Substring(0, file.Name.LastIndexOf(".")) + counter.ToString() + file.Extension;
                file.MoveTo(DestinationPath + "\\" + newName);
                Logger.Log.Info("Перемещен файл: " + file.Name + " в папку: " + DestinationPath + "имя файла в папке: " + newName);
            }

        }
    }
}
