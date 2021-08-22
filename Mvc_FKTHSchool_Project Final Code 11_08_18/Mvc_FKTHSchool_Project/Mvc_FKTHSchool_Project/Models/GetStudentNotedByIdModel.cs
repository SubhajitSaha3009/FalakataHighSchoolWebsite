using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mvc_FKTHSchool_Project.Models
{
    public class GetStudentNotedByIdModel
    {
        [Display(Name = "Enter Class")]
        [Required(ErrorMessage = "*")]
        public string ofClass { get; set; }
    }
}