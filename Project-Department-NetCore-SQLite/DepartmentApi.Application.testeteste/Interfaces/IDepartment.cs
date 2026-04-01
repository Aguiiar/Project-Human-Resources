using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DepartmentApi.Domain.Entities;


namespace DepartmentApi.Application.Interfaces
{
    public interface IDepartment
    {

        Task Save(Department department);
        Task Update(Department department);
        Task Delete(Guid id);
        Task<IEnumerable<Department>> FindAll();
        Task<Department?> FindById(Guid id);

      
    }
}
