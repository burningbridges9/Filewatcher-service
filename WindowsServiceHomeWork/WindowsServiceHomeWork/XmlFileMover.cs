using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsServiceHomeWork
{
    class XmlFileMover:FileMover
    {
		//string XmlDestinationPath { get; } //path in config
		DirectoryInfo XmlDirInfo { get; }
        /// <summary>
        /// first init
        /// </summary>
        /// <param name="curPath"></param>
        /// <param name="destPath"></param>
        public XmlFileMover(string curPath, string destPath):base(curPath, destPath) // ConfigurationManager.AppSettings["XMLDestinationPath"];
        {
            XmlDirInfo = new DirectoryInfo(destPath);
            if (!XmlDirInfo.Exists) //create folder if it != exists
            {
                XmlDirInfo.Create();
            }
        }

        public override void Move(FileInfo file)//getting file that will be moved
        {   //checking files in destination path
            int counter = 0;// counter for the same files 
            foreach (FileInfo fileSub in XmlDirInfo.GetFiles()) //search pattern
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
                string newName = file.Name.Substring(0, file.Name.LastIndexOf(".")) +  file.Extension;
                file.MoveTo(DestinationPath + "\\" + newName);
                Logger.Log.Info("Перемещен файл: " + file.Name + " в папку: " + DestinationPath + "имя файла в папке: " + newName);
            }
            else {
                string newName = file.Name.Substring(0, file.Name.LastIndexOf(".")) + counter.ToString() + file.Extension;
                file.MoveTo(DestinationPath + "\\" + newName);
                Logger.Log.Info("Перемещен файл: " + file.Name + " в папку: " + DestinationPath + "имя файла в папке: " + newName);
            }

            
            
        }
    }
}
//class monitor in it dictionary of filemoves method 