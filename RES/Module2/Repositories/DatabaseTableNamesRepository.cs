using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module2.Repositories
{
    public static class DatabaseTableNamesRepository
    {
        public static string GetTableNameByDataset(Dataset set)
        {
            switch (set)
            {
                case Dataset.SET1:
                    return "DataSet1";
                case Dataset.SET2:
                    return "DataSet2";
                case Dataset.SET3:
                    return "DataSet3";
                case Dataset.SET4:
                    return "DataSet4";
                default:
                    return "Not Found";
            }
        }
    }
}
