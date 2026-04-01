using EmployeeApi.Domain;
using EmployeeApi.Domain.Entities;
using EmployeeApi.Domain.Services;
using DTOs;
using EmployeeApi.Infrastructure.Data;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {

        private readonly EmployeeService _service;

        public EmployeeController(EmployeeService service)
        {
            _service = service;
        }


        [HttpPost]
        public async Task<IActionResult> Create(CreateEmployeeDto dto)
        {
            var employee = new Employee(


               dto.Name,
               dto.Email,
               dto.Position,
               dto.Salary,
               dto.AdmissionDate,
               dto.Situation,
               dto.DepartmentId
                );
            employee.SetAdmissionDate(dto.AdmissionDate);


            await _service.Save(employee);

            return Ok(new { message = "Employee created sucessfully" });
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var employees = await _service.GetAllWithDepartment();
            return Ok(employees);
        }


        [HttpGet("{id:guid}")]
        public async Task<IActionResult> FindById(Guid id)
        {
            var employee = await _service.FindById(id);

            if (employee == null) return NotFound(new { mensagem = "Employee Not Found" });

            return Ok(employee);
        }


        [HttpGet("name/{name}")]
        public async Task<IActionResult> FindByNameEmployeeDepartment(string name)
        {
            var employeeDepartment = await _service.FindByNameEmployeeDepartment(name);

            return Ok(employeeDepartment);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(Guid id, CreateEmployeeDto updateDTO)
        {


            var employee = new Employee(

             updateDTO.Name,
             updateDTO.Email,
             updateDTO.Position,
             updateDTO.Salary,
             updateDTO.AdmissionDate,
             updateDTO.Situation,
             updateDTO.DepartmentId

                );

            employee.SetAdmissionDate(updateDTO.AdmissionDate);

            await _service.Update(id, employee);
            return Ok(new { message = "Employee update sucessfully" });

        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _service.DeleteSoft(id);
            return NoContent();
        }


        [HttpGet("all")]
        public async Task<IActionResult> GetAllWithDepartment()
        {
            var employees = await _service.GetAllWithDepartment();
            return Ok(employees);
        }

    }
}
