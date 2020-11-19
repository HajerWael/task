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
    public class CoursesController : ControllerBase
    {
        private readonly ILogger<CoursesController> _logger;
        private readonly TaskDbContext _db_cntx;
        public CoursesController(ILogger<CoursesController> logger, TaskDbContext db_cntx)
        {
            _db_cntx = db_cntx;
            _logger = logger;


        }
        // GET: api/<EmployeesController>
        [HttpGet]
        public ActionResult Get()
        {

            var list = _db_cntx.Courses.ToList();

           return Ok(new { data = list });
            // return new string[] { "value1", "value2" };
        }

        // GET api/<EmployeesController>/5
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            var list = _db_cntx.Courses.Find(id);

            return Ok(new { data = list });
        }

        // POST api/<EmployeesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<EmployeesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<EmployeesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
