using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using task.Data.Entities;

namespace task.Data
{
    public class TaskDbContext : DbContext
    {
        public TaskDbContext(DbContextOptions<TaskDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Company>()
                        .HasMany(c => c.Employees)
                        .WithOne(e => e.Company)
                        .IsRequired();

            modelBuilder.Entity<Department>()
            .HasMany(d => d.SubDepartments)
            .WithOne(d => d.Parent)
            .HasForeignKey(d => d.ParentId);

            /*
            modelBuilder.Entity<EmployeeTask>()
                        .HasKey(et => new { et.EmployeeId, et.TaskId });
            */
        }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<StudentCourse> StudentsCourses { get; set; }
    }
}
