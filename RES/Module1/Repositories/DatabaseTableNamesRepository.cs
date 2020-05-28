using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module1.Repositories
{
    public static class DatabaseTableNamesRepository
    {
        public static string GetTableNameByDataset(Dataset set)
        {
            switch (set)
            {
                case Dataset.SET1:
                    return "res_module1_dataset1";

                case Dataset.SET2:
                    return "res_module1_dataset2";

                case Dataset.SET3:
                    return "res_module1_dataset3";

                case Dataset.SET4:
                    return "res_module1_dataset4";

                default:
                    return "Not found";
            }

        }
    }
}
