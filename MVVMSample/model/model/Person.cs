using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVVMSample.util;

namespace MVVMSample.model.model
{
    public class Person : BindableBase
    {
        private int? age;

        public int? Age
        {
            get { return this.age; }
            set { this.SetProperty(ref this.age, value); }
        }

    }
}
