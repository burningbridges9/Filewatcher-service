using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Configuration;
using System.Collections.Specialized;

namespace WindowsServiceHomeWork
{
    public partial class Service1 : ServiceBase
    {
        public TimerCallback Tm { get => tm; set => tm = value; }
        private MyFileWatcherService myFileWatcherServ { get;  }
        TimerCallback tm;
        // создаем таймер
        Timer timer;
        /// <summary>
        /// service initialization
        /// </summary>
        public Service1()
        {

            myFileWatcherServ = new MyFileWatcherService();
            InitializeComponent();
            Logger.InitLogger();//инициализация
            Logger.Log.Info("Ура заработало!");

            this.CanStop = true; // службу можно остановить
            this.CanPauseAndContinue = true; // службу можно приостановить и затем продолжить
            
        }
        
        /// <summary>
        /// Start monitor folder
        /// </summary>
        /// <param name="obj"></param>
        protected void CheckFiles(object obj)
        {
            myFileWatcherServ.MonitorFolder(ConfigurationManager.AppSettings["Dir"]);
        }

        /// <summary>
        /// init timer & timercallback
        /// </summary>
        /// <param name="args"></param>
        protected override void OnStart(string[] args)
        {
            //new Timer + CallBack
            //Tm = new TimerCallback(myFileWatcherServ.MonitorFolder);
            //timer = new Timer(tm, ConfigurationManager.AppSettings["Dir"], 0, int.Parse(ConfigurationManager.AppSettings["timePeriod"])); // onstart
            Logger.Log.Info("Starting.");
            Tm = new TimerCallback(CheckFiles);
            timer = new Timer(tm, null, 0, int.Parse(ConfigurationManager.AppSettings["timePeriod"])); // onstart
            
        }

        /// <summary>
        /// stop timer on onStop
        /// </summary>
        protected override void OnStop()
        {
            //Timer stop
           // timer.Change(System.Threading.Timeout.Infinite, 0);
            Logger.Log.Info("Stopped.");
        }

        
    }
}
