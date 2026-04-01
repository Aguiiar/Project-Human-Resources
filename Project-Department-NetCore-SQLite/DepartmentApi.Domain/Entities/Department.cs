using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepartmentApi.Domain.Entities
{
    public class Department
    {
        public Guid Id { get; private set; }
        public String Name { get; set; }
        public int AmountEmployee { get; set; }

    

        public Department() { }


        public Department(string name, int amountEmployee)
        {

            Name = name;
            AmountEmployee = amountEmployee;
      
        }
    }
}
