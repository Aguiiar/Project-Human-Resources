using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using DTOs;
using EmployeeApi.Exceptions;
using EmployeeApi.Domain.Entities;
using EmployeeApi.Application.Interfaces;
using EmployeeApi.ApplicationInterfaces;


namespace EmployeeApi.Domain.Services
{
    public class EmployeeService
    {
        private readonly IEmployee _repository;
        private readonly IDepartmentClient _departmentClient;

        public EmployeeService(IEmployee repository, IDepartmentClient departmentClient)
        {
            _repository = repository;
            _departmentClient = departmentClient;
        }


        public async Task<IEnumerable<Employee>> GetAll()
        {
            return await _repository.FindAll();
        }


        public async Task<Employee> FindById(Guid id)
        {
            var employee = await _repository.FindById(id);

            if (employee == null) throw new NotFoundException("Employee not Found");

            return employee;
        }


        public async Task<List<Employee>> FindByName(String name)
        {
            return await _repository.FindByName(name);
        }


        public async Task<List<EmployeeDepartmentDtoResponse>> FindByNameEmployeeDepartment(String name)
        {
            var employess = await _repository.FindByName(name);
            var result = new List<EmployeeDepartmentDtoResponse>();

            foreach (var employee in employess)
            {
                var department = await _departmentClient.GetDepartmentById(employee.DepartmentId);

                result.Add(new EmployeeDepartmentDtoResponse
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Email = employee.Email,
                    Position = employee.Position,
                    Salary = employee.Salary,
                    AdmissionDate = employee.AdmissionDate,
                    Situation = employee.Situation,
                    Department = department
                });
            }
            return result;
        }


        public async Task Save(Employee employee)
        {
            if (employee == null) throw new BusinessException("Employee can not be null");

            if (employee.Salary < 0) throw new BusinessException("Salary can not be negative");

            await _repository.Save(employee);



            await _departmentClient.IncrementEmployee(employee.DepartmentId);
        }



        public async Task Update(Guid id, Employee updateEmployee)
        {
            var employee = await _repository.FindById(id);

            if (employee == null) throw new NotFoundException("Employee not found");

            if (updateEmployee.Salary < 0) throw new BusinessException("Salary cannot be negative");


            employee.Name = updateEmployee.Name;
            employee.Email = updateEmployee.Email;
            employee.Position = updateEmployee.Position;
            employee.Salary = updateEmployee.Salary;
            employee.AdmissionDate = updateEmployee.AdmissionDate;
            employee.Situation = updateEmployee.Situation;
            employee.DepartmentId = updateEmployee.DepartmentId;
            await _repository.Update(employee);
        }



        public async Task DeleteSoft(Guid id)
        {

            var employee = await _repository.FindById(id);

            if (employee == null) throw new NotFoundException("Employee not found");

            employee.Situation = false;

            await _repository.DeleteSoft(employee);


            await _departmentClient.DecrementEmployee(employee.DepartmentId);

        }



        public async Task<List<EmployeeDepartmentDtoResponse>> GetAllWithDepartment()
        {

            var employees = await _repository.FindAll();
            var result = new List<EmployeeDepartmentDtoResponse>();

            foreach (var employee in employees)
            {
                var department = await _departmentClient.GetDepartmentById(employee.DepartmentId);



                result.Add(new EmployeeDepartmentDtoResponse
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Email = employee.Email,
                    Position = employee.Position,
                    Salary = employee.Salary,
                    AdmissionDate = employee.AdmissionDate,
                    Situation = employee.Situation,
                    Department = department

                });
            }

            return result;

        }






    }
}
