using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using task.Data.Entities;

namespace task.Data
{
    public class DummyData
    {
        public static void Initialize(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<TaskDbContext>();
                context.Database.EnsureCreated();

                if (context.Employees != null && context.Employees.Any())
                    return; /// DB has already been seeded



                var companies = GetCompanies().ToArray();
                context.Companies.AddRange(companies);
                context.SaveChanges();


                /*var departments = GetDepartments().ToArray();
                context.Departments.AddRange(departments);
                context.SaveChanges();*/

                var employees = GetEmployees(context).ToArray();
                context.Employees.AddRange(employees);
                context.SaveChanges();
                
                var students = GetStudents().ToArray();
                context.Students.AddRange(students);
                context.SaveChanges();

                var courses = GetCourses().ToArray();
                context.Courses.AddRange(courses);
                context.SaveChanges();


                var students_courses = GetStudentsCourses(context).ToArray();
                context.StudentsCourses.AddRange(students_courses);
                context.SaveChanges();


            }
        }

        public static List<Employee> GetEmployees(TaskDbContext db)
        {
            var companies = db.Companies.ToArray().OrderBy(d => d.Id);
            List<Employee> emps = new List<Employee>()
            {
                new Employee { name = "Emp-1" ,status = true, birthDate = "21-10-1990" ,Company = companies.ElementAt(0) },
                new Employee { name = "Emp-2" ,status = true, birthDate = "9-10-1988" ,Company = companies.ElementAt(1)  },
                new Employee { name = "Emp-4" ,status = true, birthDate = "1-1-1990" ,Company = companies.ElementAt(2) },
                new Employee { name = "Emp-5" , status = true, birthDate = "10-2-1998" ,Company = companies.ElementAt(0) },
                new Employee { name = "Emp-6" ,status = true, birthDate = "7-12-1995" ,Company = companies.ElementAt(3)  },
                new Employee { name = "Emp-7" ,status = true, birthDate = "30-48-1991" ,Company = companies.ElementAt(1)  }
            };
            // Assign Tasks to an employee
            return emps;
        }
        public static List<Department> GetDepartments()
        {
            List<Department> depts = new List<Department>()
            {
                new Department() {Name = "Dept-1" },
                new Department() {Name = "Dept-2" },
                new Department() {Name = "Dept-3"},
                new Department() {Name = "Dept-4"}

            };
            return depts;
        }

        public static List<Company> GetCompanies()
        {
            List<Company> depts = new List<Company>()
            {
                new Company() {Name = "Comp-1" },
                new Company() {Name = "Comp-2" },
                new Company() {Name = "Comp-3"},
                new Company() {Name = "Comp-4"}

            };
            return depts;
        } 
        
        public static List<Student> GetStudents()
        {
            List<Student> students = new List<Student>()
            {
                new Student() {Name = "Std-1" },
                new Student() {Name = "Std-2" },
                new Student() {Name = "Std-3"},
                new Student() {Name = "Std-4"}

            };
            return students;
        }  
        public static List<Course> GetCourses()
        {
            List<Course> courses = new List<Course>()
            {
                new Course() {Name = "Course-1" },
                new Course() {Name = "Course-2" },
                new Course() {Name = "Course-3"},
                new Course() {Name = "Course-4"}

            };
            return courses;
        }


        public static List<StudentCourse> GetStudentsCourses(TaskDbContext db)
        {
            var students = db.Students.ToArray().OrderBy(e => e.Id);
            var courses = db.Courses.ToArray().OrderBy(e => e.Id);

            List<StudentCourse> emp_tasks = new List<StudentCourse>()
            {
                new StudentCourse() {Student = students.ElementAt(0) , Course = courses.ElementAt(0) },
                new StudentCourse() {Student = students.ElementAt(0) , Course = courses.ElementAt(1) },
                new StudentCourse() {Student = students.ElementAt(1) , Course = courses.ElementAt(0) },
                new StudentCourse() {Student = students.ElementAt(1) , Course = courses.ElementAt(1) },
                
            };
            return emp_tasks;
        }

    }
}
