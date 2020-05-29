///////////////////////////////////////////////////////////
//  HistoricalCollection.cs
//  Implementation of the Class HistoricalCollection
//  Generated by Enterprise Architect
//  Created on:      18-May-2020 4:57:17 PM
//  Original author: Predrag
///////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Module2
{

    public class HistoricalCollection : IHistoricalCollection
    {

        private List<IModule2Property> properties;



        public HistoricalCollection()
        {
            properties = new List<IModule2Property>();
        }

        public HistoricalCollection(List<IModule2Property> properties)
        {
            this.properties = properties;
        }

        public List<IModule2Property> Properties
        {

            get
            {
                return properties;
            }
            set
            {
                properties = value;
            }
        }
    }
}//end HistoricalCollection