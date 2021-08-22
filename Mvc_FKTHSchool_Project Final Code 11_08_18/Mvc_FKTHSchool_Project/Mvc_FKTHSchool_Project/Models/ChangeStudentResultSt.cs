using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mvc_FKTHSchool_Project.Models
{
    public class ChangeStudentResultSt
    {
        [Display(Name="Student Id")]
        [Required(ErrorMessage="*")]
        [RegularExpression("([Ss][0-9 ]*)", ErrorMessage = "invalid format")]
        [StringLength(8, ErrorMessage = "Max 8 chars")]
        public string StudentId { get; set; }

        [Display(Name = "Choose Status")]
        [Required(ErrorMessage = "*")]
        public string Status { get; set; }
    }
}