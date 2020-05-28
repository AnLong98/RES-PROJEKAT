///////////////////////////////////////////////////////////
//  Module1DatabaseManager.cs
//  Implementation of the Class Module1DatabaseManager
//  Generated by Enterprise Architect
//  Created on:      18-May-2020 4:57:19 PM
//  Original author: Predrag
///////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data.SQLite;
using Module1.Repositories;

namespace Modul1
{


    public class Module1DatabaseManager : IModule1DatabaseManagement
    {

        private ILogging logger;
        private SQLiteConnection databaseConnection;
        private readonly string databaseName;

        /// 
        /// <param name="logger">Logger</param>
        public Module1DatabaseManager(ILogging logger, string databaseName)
        {
            this.logger = logger;
            this.databaseName = databaseName;

            if (!File.Exists(databaseName)) throw new Exception("Database does not exist");
            string path = @"C:\Users\Predrag\Source\Repos\RES-PROJEKAT\RES";
            databaseConnection = new SQLiteConnection(string.Format(@"Data Source={0}\{1};New=False;", path, databaseName));
            databaseConnection.Open();
        }

        /// 
        /// <param name="property">Module 1 property</param>
        public void WriteProperty(IModule1Property property)
        {

            logger.LogNewInfo(string.Format("Trying to write property with signal code {0} and value {1} to database", property.Code, property.Module1Value));
            Dataset set = DatasetRepository.GetDataset(property.Code);
            string tableName = DatabaseTableNamesRepository.GetTableNameByDataset(set);

            string signalCode = property.Code.ToString();
            double value = property.Module1Value;
            string query = @"DELETE FROM " + tableName + " WHERE signalCode=@codeToDelete; INSERT INTO " + tableName + " (signalCode, signalValue) VALUES(@codeToInsert, @value)";

            using (SQLiteCommand command = new SQLiteCommand(query, databaseConnection))
            {
                command.Parameters.AddWithValue("@codeToDelete", signalCode);
                command.Parameters.AddWithValue("@codeToInsert", signalCode);
                command.Parameters.AddWithValue("@value", value);

                if (command.ExecuteNonQuery() == 0)
                {
                    logger.LogNewWarning("Could not write to database.");
                }
                else
                {
                    logger.LogNewInfo("Property written successfully.");
                }
            }
        }

    }//end Module1DatabaseManager
}
