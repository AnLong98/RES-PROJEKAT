using System;
using System.Collections.Generic;


public interface IModule2DatabaseManagement
{
        IModule2Property ReadLastByCode(SignalCode code);
        List<IModule2Property> ReadPropertiesByTimeframe(DateTime periodStart, DateTime periodEnd, SignalCode code);
        void WriteProperty(IModule2Property property);
}
