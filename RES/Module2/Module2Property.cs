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



public class Module2Property : IModule2Property {

	private SignalCode code;
	private double value;



	~Module2Property(){

	}

	/// 
	/// <param name="code"></param>
	/// <param name="value"></param>
	public Module2Property(SignalCode code, double value){

	}

	public Module2Property(){

	}

	public SignalCode Code{
		get{
			return code;
		}
		set{
			code = value;
		}
	}

	public double Value{
		get{
			return value;
		}
		set{
			value = value;
		}
	}

}//end Module2Property