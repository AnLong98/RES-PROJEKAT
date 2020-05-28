///////////////////////////////////////////////////////////
//  Module1ServiceProvider.cs
//  Implementation of the Class Module1ServiceProvider
//  Generated by Enterprise Architect
//  Created on:      18-May-2020 4:57:19 PM
//  Original author: Predrag
///////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Common;

namespace Modul1
{

    public class Module1ServiceProvider : IModule1
    {

        private ILogging logging;
        private IModule2Update module2Proxy;
        private IModule1DataAdapting dataAdapter;
        private IListDescription listDescription;
        private IModule1DatabaseManagement databaseManager;

        public Module1ServiceProvider(ILogging logging, IModule2Update module2Proxy, IModule1DataAdapting dataAdapter, IListDescription listDescription, IModule1DatabaseManagement databaseManager)
        {
            this.logging = logging;
            this.module2Proxy = module2Proxy;
            this.dataAdapter = dataAdapter;
            this.listDescription = listDescription;
            this.databaseManager = databaseManager;
        }

        /// 
        /// <param name="value">Signal value</param>
        /// <param name="signalCode">Code for given signal</param>
        public bool UpdateDataset(double value, SignalCode signalCode)
        {
            logging.LogNewInfo(string.Format("Update dataset called for signal {0} and value {1}", signalCode, value));
            Dataset dataset = DatasetRepository.GetDataset(signalCode);
            IModule1Property property = dataAdapter.PackToModule1Property(signalCode, value);
            listDescription.AddOrReplaceProperty(property);

            if(listDescription.IsDatasetFull(dataset))
            {
                logging.LogNewInfo(string.Format("Dataset {0} is full, sending the whole list to module 2", dataset.ToString()));
                module2Proxy.UpdateDatabase(listDescription);
            }


            logging.LogNewInfo("Calling database manager to write new property..");
            databaseManager.WriteProperty(property);
            return true;
        }


    }//end Module1ServiceProvider
}