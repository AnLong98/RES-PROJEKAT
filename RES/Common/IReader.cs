using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public interface IReader
    {
        string ReadFromHistory(string beginDate, string endDate, int code);
    }
}
