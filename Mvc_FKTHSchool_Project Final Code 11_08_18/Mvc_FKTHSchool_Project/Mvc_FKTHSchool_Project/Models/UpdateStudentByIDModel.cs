using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mvc_FKTHSchool_Project.Models
{
    public class UpdateStudentByIDModel
    {
        [Display(Name = "Student ID")]
        [StringLength(8, ErrorMessage = "Max 8 characters")]
        [RegularExpression("([Ss][0-9]*)", ErrorMessage = "Student ID should start with 'S'/'s'")]
        [Required(ErrorMessage = "Please enter Student ID")]
        public string studentID { get; set; }
    }
}