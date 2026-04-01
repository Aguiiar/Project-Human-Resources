using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeApi.Domain.Entities;
using EmployeeApi.Application.Interfaces;
using EmployeeApi.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using EmployeeApi.ApplicationInterfaces;

namespace EmployeeApi.Infrastructure.Repositories
{
    public class EmployeeRepository : IEmployee
    {

        private readonly AppDbContext _context;


        public EmployeeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Employee>> FindAll()
        {
            return await _context.Employees.ToListAsync();
        }

        public async Task<Employee?> FindById(Guid id)
        {
            return await _context.Employees.FindAsync(id);
        }



        public async Task<List<Employee>> FindByName(string name)
        {
            return await _context.Employees.Where(e => e.Name.ToLower().Contains(name.ToLower())).OrderBy(e => e.Name).Take(3).ToListAsync();
        }

        public async Task Save(Employee employee)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
        }


        public async Task Update(Employee employee)
        {
            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();
        }


        public async Task DeleteSoft(Employee employee)
        {
     
            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();
        }

    
       
    }
}
