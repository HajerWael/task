using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using task.Data;
using task.Data.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace task.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsCoursesController : Controller
    {
        private readonly ILogger<StudentsCoursesController> _logger;
        private readonly TaskDbContext _db_cntx;
        public StudentsCoursesController(ILogger<StudentsCoursesController> logger, TaskDbContext db_cntx)
        {
            _db_cntx = db_cntx;
            _logger = logger;


        }
        // GET: api/<EmployeesController>
        [HttpGet]
        public ActionResult Get()
        {

            var list = _db_cntx.StudentsCourses.ToList();

           return Ok(new { data = list });
            // return new string[] { "value1", "value2" };
        }

        // GET api/<EmployeesController>/5
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            var list = _db_cntx.StudentsCourses.Where(sc => sc.Student.Id == id).Select(sc => sc.Course).ToList();
            var list2 = _db_cntx.StudentsCourses.Where(sc => sc.Student.Id == id).ToList();

            return Ok(new { data = list ,data2 =list2 });
        }

        // POST api/<EmployeesController>
        [HttpPost]
        public async Task<ActionResult<Employee>> Post([FromBody] StudentCourse cs)
        {
            /*var comp = _db_cntx.Companies.Find(emp.CompanyId);
            emp.Company = comp;*/
            _db_cntx.StudentsCourses.Add(cs);
            try
            {
                await _db_cntx.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return Forbid(e.ToString());
            }
            return CreatedAtAction("GetStudentsCourses", new { id = cs.Id }, cs);

        }

        // PUT api/<EmployeesController>/5
        [HttpPut]
        public async Task<ActionResult<StudentCourse>> Put(int id, [FromBody] StudentCourse sc)
        {


            var obj = _db_cntx.StudentsCourses.Attach(sc);
            if (obj == null)
            {
                return Forbid();
            }
            obj.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            try
            {
                await _db_cntx.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return Forbid(e.ToString());
            }
            return Ok(sc);

        }

        // DELETE api/<EmployeesController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<StudentCourse>> Delete(int id)
        {
            StudentCourse obj = _db_cntx.StudentsCourses.Where(sc => sc.CourseId == id).FirstOrDefault();
            if (obj == null)
            {
                return Forbid();
            }
            _db_cntx.StudentsCourses.Remove(obj);
            try
            {
                await _db_cntx.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return Forbid(e.ToString());
            }
            return Ok(obj);
        }
    }
}
