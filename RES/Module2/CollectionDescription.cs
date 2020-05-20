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
	private static int staticID = 20;
	public IHistoricalCollection collection;

	public CollectionDescription(){
        this.id = AssignId();
	}

    public CollectionDescription(Dataset dataset, HistoricalCollection collection)
    {
        this.id = AssignId();
        this.dataset = dataset;
        this.collection = collection;
    }

    ~CollectionDescription(){

	}

	public IHistoricalCollection Collection{

        get{
            return collection;
        }
        set
        {
            collection = value;
        }
	}

	public Dataset Dataset{
		get{
			return dataset;
		}
		set{
			dataset = value;
		}
	}


    public int ID{

        get
        {
            return id;
        }

        set
        {
            id = value;
        }
	}


    private static int AssignId()
    {
        int newId = staticID + 1;
        string newIDString = newId.ToString();


        if(newIDString[0] == '3')//Should add another zero and change this to 2
        {
            newIDString = "2" + newIDString.Substring(1);
            newIDString += '0';
            newId = int.Parse(newIDString);
        }

        staticID = newId;
        return newId;
    }

}//end CollectionDescription