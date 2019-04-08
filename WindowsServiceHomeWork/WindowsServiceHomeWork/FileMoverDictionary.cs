using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsServiceHomeWork
{
    /// <summary>
    /// dictionary contains different file movers
    /// </summary>
    class FileMoverDictionary
    {
        private Dictionary<string, FileMover> dictionary; // ext + form

        /// <summary>
        /// first init
        /// </summary>
        public FileMoverDictionary()
        {
            dictionary = new Dictionary<string, FileMover>();
            dictionary.Add(".txt", new TxtFileMover(ConfigurationManager.AppSettings["Dir"], ConfigurationManager.AppSettings["TXTDestinationPath"]));
            dictionary.Add(".xml", new XmlFileMover(ConfigurationManager.AppSettings["Dir"], ConfigurationManager.AppSettings["XMLDestinationPath"]));
            dictionary.Add("other", new OtherFileMover(ConfigurationManager.AppSettings["Dir"], ConfigurationManager.AppSettings["TrashDestinationPath"]));
        }
        /// <summary>
        /// add new file mover
        /// </summary>
        /// <param name="ext"></param>
        /// <param name="fm"></param>
        public void Add(string ext, FileMover fm)
        {
            dictionary.Add(ext, fm);
        }

        /// <summary>
        /// get mover by key
        /// </summary>
        /// <param name="formatter"></param>
        /// <returns></returns>
        public FileMover GetMover(string formatter) //Get
        {
            if (dictionary.ContainsKey(formatter))
            {
                return dictionary[formatter];
            }
            else
            {
                return dictionary["other"];
            }
        }
    }
}
