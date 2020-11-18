using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace task.Data.Entities
{
    public class Department
    {
        public int Id { get; set; }
        
       // [Required]
        public string Name { get; set; }
        
        [ForeignKey("Department")]
        public Department ParentDepartment { get; set; }

        public int ParentId { get; set; }

    }
}
