///////////////////////////////////////////////////////////
//  DatasetRepository.cs
//  Implementation of the Class DatasetRepository
//  Generated by Enterprise Architect
//  Created on:      18-May-2020 4:57:16 PM
//  Original author: Predrag
///////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;



public class DatasetRepository {

	public DatasetRepository(){

	}

	~DatasetRepository(){

	}

	/// 
	/// <param name="signal"></param>
	public static Dataset GetDataset(SignalCode signal){

        if (signal == SignalCode.CODE_ANALOG || signal == SignalCode.CODE_DIGITAL) return Dataset.SET1;

        if (signal == SignalCode.CODE_CUSTOM || signal == SignalCode.CODE_LIMITSET) return Dataset.SET2;

        if (signal == SignalCode.CODE_SIGNLENODE|| signal == SignalCode.CODE_MULTIPLENODE) return Dataset.SET3;

        return Dataset.SET4;
    }

}//end DatasetRepository