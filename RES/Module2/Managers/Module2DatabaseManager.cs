///////////////////////////////////////////////////////////
//  Module2DatabaseManager.cs
//  Implementation of the Class Module2DatabaseManager
//  Generated by Enterprise Architect
//  Created on:      18-May-2020 4:57:20 PM
//  Original author: Predrag
///////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Common;
using System.Data.SQLite;
using Module2.Repositories;

namespace Module2
{

    public class Module2DatabaseManager : IModule2DatabaseManagement
    {

        private ILogging logger;
        private SQLiteConnection databaseConnection;
        private readonly string databaseName;

        ~Module2DatabaseManager()
        {
            databaseConnection.Close();
        }

        public Module2DatabaseManager()
        {
            
        }

        /// 
        /// <param name="logger">Logger for this component</param>
        public Module2DatabaseManager(ILogging logger, string databasePath)
        {
            this.logger = logger;
            this.databaseName = databasePath;

            if (!File.Exists(databaseName)) throw new Exception("Database does not exist");
            databaseConnection = new SQLiteConnection(string.Format("Data Source={0}", databaseName));
            databaseConnection.Open();
        }

        /// 
        /// <param name="code">Signal code</param>
        public IModule2Property ReadLastByCode(SignalCode code)
        {
            Dataset set = DatasetRepository.GetDataset(code);
            string tableName = DatabaseTableNamesRepository.GetTableNameByDataset(set);

            string signalCode = code.ToString();
            string query = "SELECT ID, signalCode, value FROM @tableName" +
                           "WHERE signalCode=@code" +
                           "ORDER BY timestamp LIMIT 1";

            SQLiteCommand command = new SQLiteCommand(query, databaseConnection);
            command.Parameters.AddWithValue("@tableName", tableName);
            command.Parameters.AddWithValue("@code", signalCode);
            SQLiteDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                string retrievedSignal = reader["signalCode"].ToString();
                string value = reader["value"].ToString();

                SignalCode retrievedCode = (SignalCode)Enum.Parse(typeof(SignalCode), retrievedSignal);
                double valueRetrieved = double.Parse(value);
                Module2Property property = new Module2Property(retrievedCode, valueRetrieved);
                return property;
            }

            return null;
        }

        /// 
        /// <param name="periodStart">Beginning of the search period</param>
        /// <param name="periodEnd">End of the search period</param>
        public List<IModule2Property> ReadPropertiesByTimeframe(DateTime periodStart, DateTime periodEnd)
        {

            return null;
        }

        /// 
        /// <param name="property">Module2Property to be written</param>
        /// <param name="timestamp">Time when data arrived to module</param>
        public void WriteProperty(IModule2Property property, DateTime timestamp)
        {

        }

    }
    
}//end Module2DatabaseManager