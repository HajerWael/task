using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using task.Data;
using task.Data.Entities;

namespace task.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {

        private readonly ILogger<DepartmentsController> _logger;
        private readonly TaskDbContext _db_cntx;
        public DepartmentsController(ILogger<DepartmentsController> logger, TaskDbContext db_cntx)
        {
            _db_cntx = db_cntx;
            _logger = logger;


        }
        // GET: api/<DepartmentsController>
        [HttpGet]
        public ActionResult Get()
        {

           // var list = _db_cntx.Departments.Where(x => x.ParentId == null).Include(x => x.SubDepartments).ToList();
            var list = _db_cntx.Departments
               .AsEnumerable()
               .Where(x => x.ParentId == null)
               .ToList(); 

            return Ok(new { data = list });
            // return new string[] { "value1", "value2" };
        }
        
         
 

        // GET api/<DepartmentsController>/5
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            var list = _db_cntx.Departments
                //.Include(e => e.Company)
                .ToList();

            return Ok(new { data = list });
        }

        // POST api/<DepartmentsController>
        [HttpPost]
        public async Task<ActionResult<Department>> Post([FromBody] Department dept)
        {
            /*var comp = _db_cntx.Companies.Find(dept.CompanyId);
            dept.Company = comp;*/
            _db_cntx.Departments.Add(dept);
            try
            {
                await _db_cntx.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return Forbid(e.ToString());
            }
            return CreatedAtAction("GetDepartment", new { id = dept.Id }, dept);

        }

        // PUT api/<DepartmentsController>/5
        [HttpPut]
        public async Task<ActionResult<Department>> Put(int id, [FromBody] Department dept)
        {


            var obj = _db_cntx.Departments.Attach(dept);
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
            return Ok(dept);

        }

        // DELETE api/<DepartmentsController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Department>> Delete(int id)
        {
            
           
                var obj = _db_cntx.Departments.Find(id);
            
           
                if (obj == null)
                {
                    return Forbid();
                }

                IList<Department> childs = _db_cntx.Departments.Where(d => d.Parent == obj).ToList();
                _db_cntx.Departments.RemoveRange(childs);
                _db_cntx.Departments.Remove(obj);
     
           
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
