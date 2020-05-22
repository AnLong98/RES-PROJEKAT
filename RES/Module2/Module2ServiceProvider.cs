///////////////////////////////////////////////////////////
//  Module2ServiceProvider.cs
//  Implementation of the Class Module2ServiceProvider
//  Generated by Enterprise Architect
//  Created on:      18-May-2020 4:57:20 PM
//  Original author: Predrag
///////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Common;
using System.ServiceModel;

namespace Module2
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class Module2ServiceProvider : IModule2Update, IModule2History, IModule2DirectUpdate
    {

        private ILogging logger;
        public Module2DatabaseManager databaseManager;
        public Module2DataAdapter dataAdapter;
        public List<CollectionDescription> collectionDescription;
        private static readonly double deadbandPercentage = 2;
        private readonly string databaseName = "res_module2.db";

        /// 
        /// <param name="logger">Logger</param>
        public Module2ServiceProvider(ILogging logger)
        {
            this.logger = logger;
            databaseManager = new Module2DatabaseManager(logger, databaseName);
            dataAdapter = new Module2DataAdapter(logger);
            collectionDescription = new List<CollectionDescription>();
        }

        /// 
        /// <param name="oldValue">Module 2 property of the last saved value for a given signal code</param>
        /// <param name="newValue">Module 2 property of the new value for a given signal code</param>
        /// <param name="deadbandPercentage">Percentage deadband to be checked</param>
        private bool IsDeadbandSatisfied(Module2Property oldValue, Module2Property newValue, double deadbandPercentage)
        {

            double difference = Math.Abs(newValue.Value - oldValue.Value);
            double percentageDifference = (difference / oldValue.Value) * 100;

            if (percentageDifference > deadbandPercentage)
            {
                return true;
            }
            else return false;
        }

        /// 
        /// <param name="startDate">Start date</param>
        /// <param name="endDate">End date</param>
        public List<IModule2Property> ReadHistory(DateTime startDate, DateTime endDate)
        {

            return null;
        }

        /// 
        /// <param name="property"></param>
        public bool UpdateDatabase(IListDescription property)
        {
            /*logger.LogNewInfo("New list description arrived.");

            List<CollectionDescription> collectionDescriptions = dataAdapter.RepackToCollectionDescriptionArray(property);*/
            return false;
        }

        /// 
        /// <param name="code"></param>
        /// <param name="value"></param>
        public void WriteToHistory(SignalCode code, double value)
        {

        }

        /// <summary>
        /// Returns true if server is alive, used for connection testing
        /// </summary>
        public bool Ping()
        {
            return true;
        }
    }

}//end Module2ServiceProvider