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
    public class EmployeesController : ControllerBase
    {
        private readonly ILogger<EmployeesController> _logger;
        private readonly TaskDbContext _db_cntx;
        public EmployeesController(ILogger<EmployeesController> logger, TaskDbContext db_cntx)
        {
            _db_cntx = db_cntx;
            _logger = logger;


        }
        // GET: api/<EmployeesController>
        [HttpGet]
        public ActionResult Get()
        {

            var list = _db_cntx.Employees.Include(e => e.Company).ToList();

           return Ok(new { data = list });
            // return new string[] { "value1", "value2" };
        }

        // GET api/<EmployeesController>/5
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            var list = _db_cntx.Employees          
                //.Include(e => e.Company)
                .Find(id);

            return Ok(new { data = list });
        }

        // POST api/<EmployeesController>
        [HttpPost]
        public async Task<ActionResult<Employee>> Post([FromBody] Employee emp)
        {
            /*var comp = _db_cntx.Companies.Find(emp.CompanyId);
            emp.Company = comp;*/
            _db_cntx.Employees.Add(emp);
            try
            {
                await _db_cntx.SaveChangesAsync();
            }catch(Exception e)
            {
                return Forbid(e.ToString());
            }
            return CreatedAtAction("GetEmployee", new { id = emp.Id }, emp);

        }

        // PUT api/<EmployeesController>/5
        [HttpPut]
        public async Task<ActionResult<Employee>> Put(int id, [FromBody] Employee emp)
        {
 
            
            var obj = _db_cntx.Employees.Attach(emp);
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
            return Ok(emp);

        }

        // DELETE api/<EmployeesController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Employee>> Delete(int id)
        {
            var obj = _db_cntx.Employees.Find(id);
            if (obj == null)
            {
                return Forbid();
            }
            _db_cntx.Employees.Remove(obj);
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
