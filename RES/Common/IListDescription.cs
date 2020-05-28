///////////////////////////////////////////////////////////
//  IListDescription.cs
//  Implementation of the Interface IListDescription
//  Generated by Enterprise Architect
//  Created on:      18-May-2020 4:57:17 PM
//  Original author: Predrag
///////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;



public interface IListDescription  {

	/// 
	/// <param name="description"></param>
	void AddOrReplaceDescription(IDescription description);

	List<IDescription> Descriptions{
		get;
	}

	/// 
	/// <param name="dataset"></param>
	bool DoesDescriptionExist(Dataset dataset);

	/// 
	/// <param name="datset"></param>
	IDescription GetDescriptionByDataset(Dataset datset);

	/// 
	/// <param name="dataset"></param>
	bool IsDatasetFull(Dataset dataset);

}//end IListDescription