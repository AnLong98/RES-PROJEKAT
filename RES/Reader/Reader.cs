using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reader
{
    public class Reader
    {
        private ILogging logger;
        private IModule2History module2proxy;

        public Reader()
        {

        }

        ~Reader()
        {

        }

        public Reader(ILogging logger, IModule2History modul2proxy)
        {
            this.logger = logger;
            this.module2proxy = modul2proxy;
        }

        public void ActivateReading()
        {
            logger.LogNewInfo("Reader has been activated.");
        }

        public void ConnectToModul2()
        {

        }

        public void Shutdown()
        {
            logger.LogNewInfo("Reader is shutting down.");
        }


    }
}
