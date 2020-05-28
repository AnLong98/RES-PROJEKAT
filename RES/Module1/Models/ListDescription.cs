///////////////////////////////////////////////////////////
//  ListDescription.cs
//  Implementation of the Class ListDescription
//  Generated by Enterprise Architect
//  Created on:      18-May-2020 4:57:18 PM
//  Original author: Predrag
///////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Modul1
{


    public class ListDescription : IListDescription
    {

        private List<IDescription> descriptions;
        private ILogging logger;


        public ListDescription(ILogging logger)
        {
            this.logger = logger;
            descriptions = new List<IDescription>();
        }


        public ListDescription(ILogging logger, List<IDescription> descriptions)
        {
            this.logger = logger;
            this.descriptions = descriptions;
        }

        /// 
        /// <param name="description">Module 1 description</param>
        public void AddOrReplaceDescription(IDescription description)
        {
            if (DoesDescriptionExist(description.Dataset))
            {
                descriptions.RemoveAll(x => x.Dataset == description.Dataset);
            }

            descriptions.Add(description);
        }


        /// 
        /// <param name="dataset">Dataset for description</param>
        public bool DoesDescriptionExist(Dataset dataset)
        {

            foreach(IDescription description in descriptions)
            {
                if (description.Dataset == dataset) return true;
            }

            return false;
        }

        /// 
        /// <param name="dataset">Dataset for description</param>
        public IDescription GetDescriptionByDataset(Dataset dataset)
        {

            foreach(IDescription description in descriptions)
            {
                if (description.Dataset == dataset) return description;
            }

            return null;
        }

        /// 
        /// <param name="dataset">Dataset for description</param>
        public bool IsDatasetFull(Dataset dataset)
        {

            if (!DoesDescriptionExist(dataset)) return false;

            List<IModule1Property> properties = GetDescriptionByDataset(dataset).Properties;
            if (properties.Count < 2) return false;

            return true;
        }

        public List<IDescription> Descriptions {
            get
            {
                return descriptions;
            }

        }
    }//end ListDescription
}