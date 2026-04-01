using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeApi.Domain.Entities
{
    public class Employee
    {
        public Guid Id { get; private set; }
        public String Name { get; set; }
        public String Email { get; set; }

        public String Position { get; set; }
        public Decimal Salary { get; set; }
        public DateOnly AdmissionDate { get; set; }

        public Boolean Situation { get; set; }


        public Guid DepartmentId { get; set; }


        public Employee() { }

        public Employee(String name, String email, String position, Decimal salary, DateOnly admissionDate, Boolean situation, Guid departmentId)
        {
            if (salary < 0) throw new ArgumentException("price not found");
            Id = Guid.NewGuid();
            Name = name;
            Email = email;
            Position = position;
            Salary = salary;
            AdmissionDate = admissionDate;
            Situation = situation;
            DepartmentId = departmentId;
        }


        public void SetAdmissionDate(DateOnly date)
        {
            if (date > DateOnly.FromDateTime(DateTime.Now)) throw new Exception("Date not future");

            AdmissionDate = date;
        }



    }






}
