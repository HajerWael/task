using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using task.Data;
using task.Data.Entities;
using task.Models;

namespace task.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly TaskDbContext _db_cntx;

        public HomeController(ILogger<HomeController> logger, TaskDbContext db_cntx)
        {
            _db_cntx = db_cntx;
            _logger = logger;


        }

        public IActionResult Index()
        {
            var list = _db_cntx.Employees.Include(e => e.Company).ToArray();
            ViewBag.Employees = list;
            return View();
        }

        [HttpGet]
        public JsonResult getEmployees()
        {   
            var list = _db_cntx.Employees.Include(e => e.Company).ToList();

            return Json(new {  data = list });
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
