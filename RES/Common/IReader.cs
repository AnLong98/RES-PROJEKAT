using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public interface IReader
    {
        string ReadFromModule2(string date1, string date2, int code);
    }
}
