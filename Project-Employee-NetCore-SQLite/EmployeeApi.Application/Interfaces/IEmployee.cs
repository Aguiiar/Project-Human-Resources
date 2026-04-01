using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeApi.Application.Interfaces;
using EmployeeApi.Domain.Entities;


namespace EmployeeApi.ApplicationInterfaces
{
    public interface IEmployee
    {
        Task<IEnumerable<Employee>> FindAll();
        Task<Employee?> FindById(Guid id);

        Task<List<Employee>> FindByName(string name);
        Task Save(Employee employee);
        Task Update(Employee employee);
        Task DeleteSoft(Employee employee);

    }
}
