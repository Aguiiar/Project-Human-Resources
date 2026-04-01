using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EmployeeApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;



namespace EmployeeApi.Infrastructure.Data

{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var dateOnlyConverter = new ValueConverter<DateOnly, DateTime>(
                v => v.ToDateTime(TimeOnly.MinValue),
                v => DateOnly.FromDateTime(v)
                );
            modelBuilder.Entity<Employee>().Property(e => e.AdmissionDate).HasConversion(dateOnlyConverter);
        }
    }
}
