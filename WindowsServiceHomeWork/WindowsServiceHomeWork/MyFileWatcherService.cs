using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsServiceHomeWork
{
    class MyFileWatcherService
    {
        private FileMoverDictionary FMDictionary;
        public MyFileWatcherService()
        {
            FMDictionary = new FileMoverDictionary();
        }

        /// <summary>
        /// monitor folder by gettin files extension and choosing needed file mover
        /// </summary>
        /// <param name="folderPath"></param>
        public void MonitorFolder(string folderPath)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(folderPath);
            foreach (FileInfo fileInfo in dirInfo.GetFiles())
            {
                FileMover fileMover = FMDictionary.GetMover(fileInfo.Extension);
                fileMover.Move(fileInfo);
            }
        }

    }
}
