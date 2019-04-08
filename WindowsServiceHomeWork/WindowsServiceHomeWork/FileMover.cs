using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsServiceHomeWork
{
    class FileMover
    {
        protected string CurrentPath { get; set; }
        protected string DestinationPath { get; } //path in config
        public FileMover(string curPath, string destPath)
        {
            //CurrentPath = ConfigurationManager.AppSettings["Dir"]; // inner initialization
            CurrentPath = curPath;
            //DestinationPath = ConfigurationManager.AppSettings["XMLDestinationPath"];
            DestinationPath = destPath;
        }

        public virtual void Move(FileInfo file) { }
    }
}
