using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DepartmentApi.Domain.Entities;
using DepartmentApi.Application.Interfaces;
using DepartmentApi.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;



namespace DepartmentApi.Infrastructure.Repositories
{
    public class DepartmentRepository : IDepartment
    {

        private readonly AppDbContext _context;

        public DepartmentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task Save(Department department)
        {
            _context.Departments.Add(department);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Department department)
        {
            _context.Departments.Update(department);
            await _context.SaveChangesAsync();
        }



        public async Task Delete(Guid id)
        {
            var department = await _context.Departments.FindAsync(id);
            if (department == null) throw new Exception("Department not Found");
            _context.Departments.Remove(department);

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Department>> FindAll()
        {
            return await _context.Departments.ToListAsync();
        }

        public async Task<Department?> FindById(Guid id)
        {
            return await _context.Departments.FindAsync(id);
        }


    }
}
