///////////////////////////////////////////////////////////
//  CollectionDescription.cs
//  Implementation of the Class CollectionDescription
//  Generated by Enterprise Architect
//  Created on:      18-May-2020 4:57:16 PM
//  Original author: Predrag
///////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;



public class CollectionDescription : ICollectionDescription {

	private Dataset dataset;
	private int id;
	private static int staticID;
	public HistoricalCollection collection;

	public CollectionDescription(){

	}

	~CollectionDescription(){

	}

	public IHistoricalCollection Collection(){

		return null;
	}

	public Dataset Dataset{
		get{
			return dataset;
		}
		set{
			dataset = value;
		}
	}

	public int ID(){

		return 0;
	}

}//end CollectionDescription