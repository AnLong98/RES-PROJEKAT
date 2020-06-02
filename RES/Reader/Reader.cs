using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReaderNS
{
    public class Reader : IReader
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

        public string ReadFromHistory(string beginDate, string endDate, int code)
        {
            string ret = "";
            DateTime firstDate, secondDate;
            SignalCode sCode;
            List<IModule2Property> list = new List<IModule2Property>();

            logger.LogNewInfo("Reader is validating data.");

            ValidateParameters(beginDate, endDate, code);

            logger.LogNewInfo("Reader successfully validated data.");

            firstDate = DateTime.Parse(beginDate);
            secondDate = DateTime.Parse(endDate);
            sCode = (SignalCode)code;

            list = module2proxy.ReadHistory(firstDate, secondDate, sCode);

            logger.LogNewInfo("Reader took data from Module2 database.");

            foreach (IModule2Property p in list)
            {
                ret += String.Format("Code: {0}\t Value: {1}\n", p.Code, p.Value);
            }

            return ret;
        }

        public void ValidateParameters(string beginDate, string endDate, int code)
        {
            if (!DateTime.TryParse(beginDate, out DateTime firstDate))
            {
                logger.LogNewWarning("Reader: Invalid value for startDate.");
                throw new Exception("The first date value is not valid!");
            }
            else if (!DateTime.TryParse(endDate, out DateTime secondDate))
            {
                logger.LogNewWarning("Reader: Invalid value for endDate.");
                throw new Exception("The second date value is not valid!");
            }
            else if (DateTime.Compare(firstDate, secondDate) >= 0)
            {
                logger.LogNewWarning("Reader: Ending date has to be older than starting date.");
                throw new Exception("Ending date has to be older than starting date.");
            }
            else if (code < 0 || code > 7)
            {
                logger.LogNewWarning("Reader: Invalid value for code.");
                throw new Exception("The value of code is not in range!");
            }
        }
    }
}
