using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DepartmentApi.Domain.Entities;
using DepartmentApi.Application.Interfaces;


namespace DepartmentApi.Domain.Services
{
    public class DepartmentService
    {
        private readonly IDepartment _repository;

        public DepartmentService(IDepartment repository)
        {
            _repository = repository;
        }

        public async Task Save(Department department)
        {

            if (department == null) throw new Exception("Department can't be null");

            if (string.IsNullOrWhiteSpace(department.Name)) throw new Exception("Name is requerid");


            await _repository.Save(department);
        }

        public async Task Update(Guid id, Department updateDepartment)
        {
            var department = await _repository.FindById(id);

            department.Name = updateDepartment.Name;
            department.AmountEmployee = updateDepartment.AmountEmployee;

            await _repository.Update(department);
        }

        public async Task Delete(Guid id)
        {

            var department = await _repository.FindById(id);


            if (department?.AmountEmployee > 0) throw new Exception("impossible to delete, there are employees");

            await _repository.Delete(id);


        }


        public async Task<IEnumerable<Department>> FindAll()
        {
            return await _repository.FindAll();
        }

        public async Task<Department> FindById(Guid id)
        {
            var department = await _repository.FindById(id);

            return department;
        }



        public async Task DecrementEmployee(Guid id)
        {
            var department = await _repository.FindById(id);

            if (department == null) throw new Exception("Department not found");

            if (department.AmountEmployee > 0)
                department.AmountEmployee--;

            await _repository.Update(department);
        }


        public async Task IncrementEmployee(Guid id)
        {
            var department = await _repository.FindById(id);

            if (department == null) throw new Exception("Department not found");

            department.AmountEmployee++;

            await _repository.Update(department);
        }
    }
}
