///////////////////////////////////////////////////////////
//  Module2DataAdapter.cs
//  Implementation of the Class Module2DataAdapter
//  Generated by Enterprise Architect
//  Created on:      18-May-2020 4:57:20 PM
//  Original author: Predrag
///////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Common;

namespace Module2 {
    public class Module2DataAdapter {

	    private ILogging logger;

	    public Module2DataAdapter(){

	    }

	    ~Module2DataAdapter(){

	    }

	    /// 
	    /// <param name="logger"></param>
	    public Module2DataAdapter(ILogging logger){
            this.logger = logger;
	    }

	    /// 
	    /// <param name="signal"></param>
	    /// <param name="value"></param>
	    public Module2Property PackToModule2Property(SignalCode signal, double value){

		    return null;
	    }

	    /// 
	    /// <param name="description"></param>
	    public CollectionDescription RepackToCollectionDescription(IDescription description){

		    return null;
	    }

	    /// 
	    /// <param name="listDescription"></param>
	    public CollectionDescription RepackToCollectionDescriptionArray(IListDescription listDescription){

		    return null;
	    }

	    /// 
	    /// <param name="module1Property"></param>
	    public Module2Property RepackToModule2Property(IModule1Property module1Property){

		    return null;
	    }

    }//end Module2DataAdapter
}


