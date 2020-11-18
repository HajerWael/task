﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace task.Data.Entities
{
    public class Student
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    
    }
}