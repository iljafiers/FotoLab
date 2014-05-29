using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FotoWebservice.Lib
{
    interface IDataProvider
    {
        void Open();
        void Close();
        DataSet Query();
        bool IsOpen();
    }
}
