using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMSample.model.service
{
    class Calculation
    {
        internal static int Sum(int? v1, int? v2)
        {
            return v1.GetValueOrDefault(0) + v2.GetValueOrDefault(0);
        }
    }
}
