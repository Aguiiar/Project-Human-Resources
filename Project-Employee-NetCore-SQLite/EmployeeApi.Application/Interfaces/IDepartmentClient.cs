using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTOs;




namespace EmployeeApi.Application.Interfaces
{
    public interface IDepartmentClient
    {
        Task<bool> DepartmentExists(Guid department);

        Task<DepartmentDto> GetDepartmentById(Guid departmentId);


        Task DecrementEmployee(Guid departmentID);


        Task IncrementEmployee(Guid deparmentID);


    }
}
