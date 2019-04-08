using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsServiceHomeWork
{
    class OtherFileMover:FileMover
    {
        //string XmlDestinationPath { get; } //path in config
        DirectoryInfo OtherDirInfo { get; }
        public OtherFileMover(string curPath, string destPath) : base(curPath, destPath) // ConfigurationManager.AppSettings["XMLDestinationPath"];
        {
            //XmlDestinationPath = ConfigurationManager.AppSettings["XMLDestinationPath"]; //remove
            // XmlDestinationPath = destPath;
            OtherDirInfo = new DirectoryInfo(destPath);
            if (!OtherDirInfo.Exists)
            {
                OtherDirInfo.Create();
            }
        }

        public override void Move(FileInfo file)//getting file that will be moved
        {   //checking files in destination pathe
            int counter = 0;// counter for the same files 
            foreach (FileInfo fileSub in OtherDirInfo.GetFiles("*.*")) //search pattern
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
                Logger.Log.Info("Перемещен файл: " + file.Name + " в папку: " + DestinationPath + " имя файла в папке: " + newName);
            }
            else
            {
                string newName = file.Name.Substring(0, file.Name.LastIndexOf(".")) + counter.ToString() + file.Extension;
                file.MoveTo(DestinationPath + "\\" + newName);
                Logger.Log.Info("Перемещен файл: " + file.Name + " в папку: " + DestinationPath + " имя файла в папке: " + newName);
            }

        }
    }
}
