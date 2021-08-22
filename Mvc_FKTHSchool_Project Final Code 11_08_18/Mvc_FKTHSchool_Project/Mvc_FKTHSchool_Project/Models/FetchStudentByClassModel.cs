using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mvc_FKTHSchool_Project.Models
{
    public class FetchStudentByClassModel
    {
        [Display(Name="Select Class")]
        [Required(ErrorMessage="*")]
        public int ofClass { get; set; }
    }
}