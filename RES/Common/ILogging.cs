///////////////////////////////////////////////////////////
//  ILogging.cs
//  Implementation of the Interface ILogging
//  Generated by Enterprise Architect
//  Created on:      19-May-2020 3:44:02 PM
//  Original author: Predrag
///////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;


namespace Common
{
    public interface ILogging  {

	    /// 
	    /// <param name="text"></param>
	    void LogNewInfo(string text);

	    /// 
	    /// <param name="text"></param>
	    void LogNewWarning(string text);
    }//end ILogging

}
