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
        private string firstName;

        public string FirstName
        {
            get { return this.firstName; }
            set { this.SetProperty(ref this.firstName, value); }
        }

        private string lastName;

        public string LastName
        {
            get { return this.lastName; }
            set { this.SetProperty(ref this.lastName, value); }
        }



        public string CreateFullName() {
            return firstName + " " + lastName;
        }

        public Person() {
            this.PropertyChanged += (sender, e) => {
                Console.WriteLine("change:Person");
            };
        }

    }
}
