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
        private IModule2DatabaseManagement databaseManager;
        private IModule2DataAdapting dataAdapter;
        private static readonly double deadbandPercentage = 2;
        private readonly string databaseName = "res_module2.db";

        /// 
        /// <param name="logger">Logger</param>
        public Module2ServiceProvider(ILogging logger)
        {
            this.logger = logger;
            databaseManager = new Module2DatabaseManager(logger, databaseName);
            dataAdapter = new Module2DataAdapter(logger);
        }

        public Module2ServiceProvider(ILogging logger, IModule2DatabaseManagement databaseManager, IModule2DataAdapting dataAdapter)
        {
            this.logger = logger;
            this.databaseManager = databaseManager;
            this.dataAdapter = dataAdapter;
        }

        /// 
        /// <param name="oldValue">Module 2 property of the last saved value for a given signal code</param>
        /// <param name="newValue">Module 2 property of the new value for a given signal code</param>
        /// <param name="deadbandPercentage">Percentage deadband to be checked</param>
        public bool IsDeadbandSatisfied(IModule2Property oldValue, IModule2Property newValue, double deadbandPercentage)
        {
            logger.LogNewInfo(string.Format("Checking deadband for old value {0}, and new value {1}, with percentage {2}", oldValue.Value, newValue.Value, deadbandPercentage));

            double difference = Math.Abs(newValue.Value - oldValue.Value);
            double percentageDifference = (difference / oldValue.Value) * 100;

            if (percentageDifference > deadbandPercentage)
            {
                logger.LogNewInfo("Deadband satisfied");
                return true;
            }
            else
            {
                logger.LogNewInfo("Deadband not satisfied");
                return false;
            }

        }

        /// 
        /// <param name="startDate">Start date</param>
        /// <param name="endDate">End date</param>
        public List<IModule2Property> ReadHistory(DateTime startDate, DateTime endDate)
        {

            logger.LogNewInfo(string.Format("Request arrived to read from database with start date {0} and end date {1}", startDate, endDate));
            List<IModule2Property> returnList = new List<IModule2Property>();

            foreach (IModule2Property module2Property in databaseManager.ReadPropertiesByTimeframe(startDate, endDate))
            {
                returnList.Add(module2Property);
            }

            return returnList;
        }

        /// 
        /// <param name="property">List description form module1</param>
        public bool UpdateDatabase(IListDescription property)
        {
            logger.LogNewInfo("New list description arrived. Performing update on database..");
            IModule2Property lastProperty = null;
            List<ICollectionDescription> collectionDescriptions = null;


            try
            {
                collectionDescriptions = dataAdapter.RepackToCollectionDescriptionArray(property);
            }catch(ArgumentException)
            {
                logger.LogNewWarning("Argument exception thrown by data adapter, aborting all operations.");
                return false;
            }catch(Exception)
            {
                logger.LogNewWarning("Unknown exception thrown by data adapter, aborting all operations.");
                return false;
            }
            
            List<IModule2Property> allProperties = new List<IModule2Property>();
            
            foreach(ICollectionDescription cd in collectionDescriptions)
            {
                allProperties.AddRange(cd.Collection.Properties);
            }

            foreach(IModule2Property module2property in allProperties)
            {
                lastProperty = databaseManager.ReadLastByCode(module2property.Code);
                if(lastProperty == null)
                {
                    logger.LogNewInfo(string.Format("No property found in database for signal code {0}. Writing directly without deadband checking..", module2property.Code));
                    databaseManager.WriteProperty(module2property, DateTime.Now);
                }
                else if(IsDeadbandSatisfied(lastProperty, module2property, deadbandPercentage))
                {
                    databaseManager.WriteProperty(module2property, DateTime.Now);
                }
            }
            return true;
        }

        /// 
        /// <param name="code">Signal code</param>
        /// <param name="value">Signal value</param>
        public void WriteToHistory(SignalCode code, double value)
        {
            logger.LogNewInfo(string.Format("Writing to history called for code {0} and value {1}", code, value));
            IModule2Property property = dataAdapter.PackToModule2Property(code, value);
            databaseManager.WriteProperty(property, DateTime.Now);
        }

        /// <summary>
        /// Returns true if server is alive, used for connection testing
        /// </summary>
        public bool Ping()
        {
            logger.LogNewInfo("Server pinged.");
            return true;
        }
    }

}//end Module2ServiceProvider