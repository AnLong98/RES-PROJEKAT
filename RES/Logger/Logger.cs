///////////////////////////////////////////////////////////
//  Logger.cs
//  Implementation of the Class Logger
//  Generated by Enterprise Architect
//  Created on:      19-May-2020 3:44:02 PM
//  Original author: Predrag
///////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Common;

namespace LoggerSpace
{
    public class Logger : ILogging {

	    private LogWriter writer;

	    public Logger(){

	    }

	    ~Logger(){

	    }

	    /// 
	    /// <param name="filename">Name of the file where log data will be stored</param>
	    public Logger(string filename){
            writer = new LogWriter(filename);
	    }


	    public void LogNewInfo(string text){

            LogMessage message = new LogMessage(text);
            writer.WriteToFile(message.GetInfoMessage());
	    }


	    public void LogNewWarning(string text){
            LogMessage message = new LogMessage(text);
            writer.WriteToFile(message.GetWarningMessage());
        }

    }//end Logger
}
