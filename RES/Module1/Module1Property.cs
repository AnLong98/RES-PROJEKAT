///////////////////////////////////////////////////////////
//  Module1Property.cs
//  Implementation of the Class Module1Property
//  Generated by Enterprise Architect
//  Created on:      18-May-2020 4:57:19 PM
//  Original author: Predrag
///////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Modul1
{

    public class Module1Property : IModule1Property
    {

        private SignalCode code;
        private double module1Value;



        ~Module1Property()
        {

        }

        public Module1Property()
        {

        }

        /// 
        /// <param name="code"></param>
        /// <param name="value"></param>
        public Module1Property(SignalCode code, double value)
        {

        }

        public SignalCode Code
        {
            get
            {
                return code;
            }
            set
            {
                code = value;
            }
        }

        public double Module1Value
        {
            get
            {
                return module1Value;
            }
            set
            {
                module1Value = value;
            }
        }

    }//end Module1Property
}