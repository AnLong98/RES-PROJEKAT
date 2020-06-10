///////////////////////////////////////////////////////////
//  Input.cs
//  Implementation of the Class Input
//  Generated by Enterprise Architect
//  Created on:      18-May-2020 4:57:18 PM
//  Original author: Predrag
///////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Common;
using System.Threading;
using System.Threading.Tasks;

namespace InputNS
{
    public class Input : IInput
    {

        private IModule2DirectUpdate historyWritingProxy;
        private ILogging logger;
        private IModule1 module1Proxy;
        Thread t;

        public Input()
        {
            
        }

        ~Input()
        {

        }

        /// 
        /// <param name="logger"></param>
        /// <param name="historyProxy"></param>
        /// <param name="module1Proxy"></param>
        public Input(ILogging logger, IModule2DirectUpdate historyProxy, IModule1 module1Proxy)
        {
            this.logger = logger;
            historyWritingProxy = historyProxy;
            this.module1Proxy = module1Proxy;
        }
        
        public void GenerateSignals()
        {
            while (true)
            {
                Random rand = new Random();

                int code = rand.Next(8);
                double value = 0;

                if (code != 1)
                {
                    value = rand.NextDouble() * 1001;
                }
                else
                {
                    value = rand.Next(0, 1);
                }
                logger.LogNewInfo(String.Format("Input started generating signals and sending it to Modul1 with values {0} - {1}.", code, value));
                module1Proxy.UpdateDataset(value, (SignalCode)code);

                Thread.Sleep(3000);
            }
        }

        public void StartDataFlow()
        {
            t = new Thread(GenerateSignals)
            {
                IsBackground = true
            };
            t.Start();
        }

        public void StopDataFlow()
        {
            logger.LogNewInfo("Input stopped generating signals and sending it to Modul1.");
            t.Abort();
        }

        public void SendSignal(int signal, double value)
        {
            if(value < 0)
            {
                logger.LogNewWarning("User sent invalid data for value.");
                throw new Exception("The value does not match specified interval!");
            }
            else if(signal < 0 || signal > 7)
            {
                logger.LogNewWarning("User sent invalid data for signal.");
                throw new Exception("The value of signal does not match specified interval!");
            }
            else
            {
                logger.LogNewInfo(String.Format("Input sending signal directly to Modul2 with values {0} - {1}.", (SignalCode)signal, value));
                historyWritingProxy.WriteToHistory((SignalCode)signal, value);
            }
        }
        
    }//end Input
}