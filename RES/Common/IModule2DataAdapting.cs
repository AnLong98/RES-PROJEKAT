using System.Collections.Generic;


public interface IModule2DataAdapting
{        IModule2Property PackToModule2Property(SignalCode signal, double value);
        ICollectionDescription RepackToCollectionDescription(IDescription description);
        List<ICollectionDescription> RepackToCollectionDescriptionArray(IListDescription listDescription);
        IModule2Property RepackToModule2Property(IModule1Property module1Property);
}
