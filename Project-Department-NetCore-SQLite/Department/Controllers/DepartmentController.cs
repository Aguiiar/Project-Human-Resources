
using DepartmentApi.Application.DTOs;
using DepartmentApi.Domain.Entities;
using DepartmentApi.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using SQLitePCL;

namespace Department.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentController : ControllerBase
    {

        private readonly DepartmentService _service;

        public DepartmentController(DepartmentService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Save(CreateDepartmentDto dto)
        {
            var department = new DepartmentApi.Domain.Entities.Department(

                dto.Name,
                dto.AmountEmployee

                );
            await _service.Save(department);

            return Ok(new { message = "Department Save sucessfully" });
        }



        [HttpGet("{id}")]
        public async Task<IActionResult> FindById(Guid id)
        {
            var department = await _service.FindById(id);

            if (department == null) return NotFound();

            return Ok(department);
        }



        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var departments = await _service.FindAll();
            return Ok(departments);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _service.Delete(id);
            return Ok(new { message = "Employee deleted sucessfully" });
        }



        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, CreateDepartmentDto updateDepartment)
        {
            var department = new DepartmentApi.Domain.Entities.Department(

                updateDepartment.Name,
                updateDepartment.AmountEmployee
                );

            await _service.Update(id, department);
            return Ok(new { message = "Department update sucessfuly" });
        }



        [HttpPost("{id}/decrement")]
        public async Task<IActionResult> DecrementEmployee(Guid id)
        {
            await _service.DecrementEmployee(id);
            return NoContent();
        }


        [HttpPost("{id}/increment")]
        public async Task<IActionResult> IncrementEmployee(Guid id)
        {
            await _service.IncrementEmployee(id);
            return NoContent();
        }
    }
}
