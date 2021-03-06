///////////////////////////////////////////////////////////
//  Module2Property.cs
//  Implementation of the Class Module2Property
//  Generated by Enterprise Architect
//  Created on:      18-May-2020 4:57:20 PM
//  Original author: Predrag
///////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Module2
{

    public class Module2Property : IModule2Property
    {

        private SignalCode code;
        private double codeValue;


        /// 
        /// <param name="code">Signal code for property</param>
        /// <param name="value">Value for property</param>
        public Module2Property(SignalCode code, double value)
        {
            this.code = code;
            this.codeValue = value;
        }

        public Module2Property()
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

        public double Value
        {
            get
            {
                return codeValue;
            }
            set
            {
                codeValue = value;
            }
        }
    }

}//end Module2Property