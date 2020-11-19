using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using task.Data;
using task.Data.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace task.Controllers.API
{
   // [EnableCors("CompanyPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly ILogger<StudentsController> _logger;
        private readonly TaskDbContext _db_cntx;
        public StudentsController(ILogger<StudentsController> logger, TaskDbContext db_cntx)
        {
            _db_cntx = db_cntx;
            _logger = logger;


        }
        // GET: api/<StudentsController>
        [HttpGet]
        public ActionResult Get()
        {

            var list = _db_cntx.Students.ToList();

           return Ok(new { data = list });
            // return new string[] { "value1", "value2" };
        }



        // GET: api/<StudentsController>
        /*[HttpGet]
        public ActionResult GetStudentsCourses()
        {

            var list = _db_cntx.StudentsCourses.Include(sc => sc.Student).Select(sc => sc.Course).ToList();

            return Ok(new { data = list });
            // return new string[] { "value1", "value2" };
        }*/




        // GET api/<StudentsController>/5
        [HttpGet("{id}")]
        public ActionResult GetStudentCourses(int id)
        {
            var list = _db_cntx.StudentsCourses
                .Where(sc => sc.Student.Id == id)
                .ToList();

            return Ok(new { data = list });
        }

        // GET api/<StudentsController>/5
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            var list = _db_cntx.Students          
                //.Include(e => e.Company)
                .Find(id);

            return Ok(new { data = list });
        }

        // POST api/<StudentsController>
        [HttpPost]
        public async Task<ActionResult<Student>> Post([FromBody] Student std)
        {
            /*var comp = _db_cntx.Companies.Find(emp.CompanyId);
            emp.Company = comp;*/
            _db_cntx.Students.Add(std);
            try
            {
                await _db_cntx.SaveChangesAsync();
            }catch(Exception e)
            {
                return Forbid(e.ToString());
            }
            return CreatedAtAction("GetStudent", new { id = std.Id }, std);

        }

        // PUT api/<StudentsController>/5
        [HttpPut]
        public async Task<ActionResult<Student>> Put(int id, [FromBody] Student std)
        {
 
            
            var obj = _db_cntx.Students.Attach(std);
            if(obj == null)
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
            return Ok(std);

        }

        // DELETE api/<StudentsController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Student>> Delete(int id)
        {
            var obj = _db_cntx.Students.Find(id);
            if (obj == null)
            {
                return Forbid();
            }
            IList<StudentCourse> courses = _db_cntx.StudentsCourses.Where(c => c.StudentId == id).ToList();
            _db_cntx.StudentsCourses.RemoveRange(courses);
            _db_cntx.Students.Remove(obj);
            try
            {
                await _db_cntx.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return Forbid(e.ToString());
            }
            return  Ok(obj);
        }
    }
}
