///////////////////////////////////////////////////////////
//  Description.cs
//  Implementation of the Class Description
//  Generated by Enterprise Architect
//  Created on:      18-May-2020 4:57:16 PM
//  Original author: Predrag
///////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Common;

namespace Modul1
{

    public class Description : IDescription
    {

        private Dataset dataset;
        private int id;
        private static int staticID = 10;
        private ILogging logger;
        private List<IModule1Property> propertyList;

        /// 
        /// <param name="dataset">Dataset</param>
        public Description(Dataset dataset, ILogging logger)
        {
            this.dataset = dataset;
            this.logger = logger;
            propertyList = new List<IModule1Property>();
            this.id = CreateUniqueID(staticID);
            staticID = id;
        }

        /// 
        /// <param name="property">Module 1 property</param>
        public void AddOrReplaceProperty(IModule1Property property)
        {
            logger.LogNewInfo(string.Format("Add or replace property called with signal code {0} and value {1}", property.Code, property.Module1Value ));
            if(DoesPropertyExist(property.Code))
            {
                Properties.RemoveAll(x => x.Code == property.Code);
            }

            Properties.Add(property);
        }

        public static int CreateUniqueID(int staticID)
        {

            if (staticID < 10) throw new ArgumentException("static id should be double digit value at least");

            int newId = staticID + 1;
            int firstNumber = int.Parse(staticID.ToString().Substring(0, 1));
            string newIDString = newId.ToString();


            if (int.Parse(newIDString.Substring(0, 1)) == firstNumber + 1)//Should add another zero and decrement first number
            {
                newIDString = firstNumber.ToString() + newIDString.Substring(1);
                newIDString += '0';
                newId = int.Parse(newIDString);
            }

            return newId;
        }

        public static void ResetStaticID()
        {

            staticID = 10;
        }

        /// 
        /// <param name="code">Signal code for property</param>
        public bool DoesPropertyExist(SignalCode code)
        {
            logger.LogNewInfo(string.Format("Checking if property exists for signal {0}", code));
            foreach(IModule1Property property in propertyList)
            {
                if (property.Code == code)
                {
                    logger.LogNewInfo("Property found.");
                    return true;
                }

            }
            logger.LogNewInfo("Property not found.");
            return false;
        }

        
        /*Getters and setters*/
        /// 
        /// <param name="code">Signal code for property</param>
        public IModule1Property GetPropertyByCode(SignalCode code)
        {
            logger.LogNewInfo(string.Format("Trying to get property for signal {0}", code));

            foreach(IModule1Property property in propertyList)
            {
                if (property.Code == code) return property;
            }

            return null;
            
        }

        public int ID
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }

        public List<IModule1Property> Properties
        {
            get { return propertyList; }
            set { propertyList = value; }
        }


        public Dataset Dataset
        {
            get
            {
                return dataset;
            }
            set
            {
                dataset = value;
            }
        }

    }//end Description

}