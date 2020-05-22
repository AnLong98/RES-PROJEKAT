///////////////////////////////////////////////////////////
//  IModule1.cs
//  Implementation of the Interface IModule1
//  Generated by Enterprise Architect
//  Created on:      18-May-2020 4:57:17 PM
//  Original author: Predrag
///////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.ServiceModel;

[ServiceContract]
public interface IModule1  {

    [OperationContract]
    /// 
    /// <param name="value"></param>
    /// <param name="signalCode"></param>
    bool UpdateDataset(double value, SignalCode signalCode);

    [OperationContract]
    bool Ping();
}//end IModule1