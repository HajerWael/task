using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace task.Data.Entities
{
    public class Employee
    {
        public int Id { get; set; }
        
        public string name { get; set; }
        public string birthDate { get; set; }
        public bool status { get; set; }

         
        public Company Company { get; set; }
        [ForeignKey("Company")]
        public int CompanyId { get; set; }
    }
}
